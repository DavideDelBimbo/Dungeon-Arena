using System.Collections;
using UnityEngine;

public class DeadState : CharacterState {
    [Header("Dead Settings")]
    [SerializeField] private GameObject _deadParticlesEffect;

    private bool _isExiting = false;


    public override void OnEnter() {
        base.OnEnter();
        _context.Movement.CurrentDirection = Vector2.zero;
    }

    public override void OnUpdate() {
        base.OnUpdate();
    
        // Exit the dead state when the dead animation is finished.
        if (!StateAnimation.AnimatedSprite.IsPlaying && !_isExiting) {
            _isExiting = true;
            OnExit();
        }
    }

    public override void OnExit() {
        base.OnExit();

        // Instantiate the dead particles effect and destroy the agent.
        StartCoroutine(DeadCoroutine());
    }


    private IEnumerator DeadCoroutine() {
        // Instantiate the dead particles effect.
        GameObject deadParticles = Instantiate(_deadParticlesEffect, _context.transform.position, Quaternion.identity);
        deadParticles.transform.SetParent(_context.transform);

        // Wait for the dead particles effect to finish.
        ParticleSystem parts = deadParticles.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(parts.main.duration + parts.main.startLifetime.constant);

        // Destroy the agent.
        Destroy(((MonoBehaviour) _context.GetComponentInParent<IAgent>()).gameObject);
        yield break;
    }
}