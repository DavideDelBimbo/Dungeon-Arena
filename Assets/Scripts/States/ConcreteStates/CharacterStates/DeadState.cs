using System.Collections;
using UnityEngine;
using DungeonArena.Interfaces;

namespace DungeonArena.States.CharacterStates {
    public class DeadState : CharacterState {
        [Header("Dead Settings")]
        [SerializeField] private GameObject _destroyParticlesVFX;
        [SerializeField] private Color _destroyParticlesVFXColor = Color.white;

        public override void OnEnter() {
            base.OnEnter();

            // Set the dead animation.
            _context.Movement.CurrentDirection = Vector2.zero;

            // Disable all the colliders (except for the kinematic ones).
            foreach (Collider2D collider in _context.GetComponentsInChildren<Collider2D>()) {
                if (collider.isTrigger) collider.enabled = false;
            }
        }

        public override void OnUpdate() {
            base.OnUpdate();
        
            // Transition to the end state when the dead animation finishes.
            if (!StateAnimation.AnimatedSprite.IsPlaying) {
                _context.StateMachine.TransitionToState(null);
            }
        }

        public override void OnExit() {
            base.OnExit();

            // Disable kinematic collision.
            foreach (Collider2D collider in _context.GetComponentsInChildren<Collider2D>()) {
                if (!collider.isTrigger) collider.enabled = false;
            }

            // Instantiate the dead particles VFX and destroy the agent.
            StartCoroutine(DeadCoroutine());
        }


        // Instantiate the dead particles VFX and destroy the agent.
        private IEnumerator DeadCoroutine() {
            // Call the OnDeath event on the agent.
            IAgent agent = _context.GetComponentInParent<IAgent>();
            agent.OnDeath?.Invoke(agent);

            // Disable the shadow sprite.
            _context.ShadowSprite.gameObject.SetActive(false);

            // Instantiate the dead particles VFX.
            Vector2 position = transform.GetComponentInChildren<Renderer>().bounds.center;
            GameObject deadParticles = Instantiate(_destroyParticlesVFX, position, Quaternion.identity);

            // Wait for the dead particles VFX to finish.
            ParticleSystem.MainModule parts = deadParticles.GetComponent<ParticleSystem>().main;
            parts.startColor = _destroyParticlesVFXColor;
            yield return new WaitForSeconds(parts.duration + parts.startLifetime.constant);

            // Die.
            agent.Die();
            yield break;
        }
    }
}