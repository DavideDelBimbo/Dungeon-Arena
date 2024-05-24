using System.Collections;
using UnityEngine;

public class DeadState : CharacterState {
    [Header("Dead Settings")]
    [SerializeField] private GameObject _destroyParticlesEffect;
    [SerializeField] private Color _destroyParticlesEffectColor = Color.white;

    public override void OnEnter() {
        base.OnEnter();

        // Set the dead animation.
        _context.Movement.CurrentDirection = Vector2.zero;
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

        // Instantiate the dead particles effect and destroy the agent.
        StartCoroutine(DeadCoroutine());
    }


    // Instantiate the dead particles effect and destroy the agent.
    private IEnumerator DeadCoroutine() {
        // Instantiate the dead particles effect.
        GameObject deadParticles = Instantiate(_destroyParticlesEffect, _context.transform.position, Quaternion.identity);
        deadParticles.transform.SetParent(_context.transform);

        // Wait for the dead particles effect to finish.
        ParticleSystem.MainModule parts = deadParticles.GetComponent<ParticleSystem>().main;
        parts.startColor = _destroyParticlesEffectColor;
        yield return new WaitForSeconds(parts.duration + parts.startLifetime.constant);

        // Destroy the agent.
        _context.GetComponentInParent<IAgent>().Die();
        yield break;
    }
}