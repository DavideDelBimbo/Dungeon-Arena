using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : CharacterState {
    [Header("Dead Settings")]
    [SerializeField] private GameObject _destroyParticlesEffect;
    [SerializeField] private Color _destroyParticlesEffectColor = Color.white;

    public override void OnEnter() {
        base.OnEnter();

        // Set the dead animation.
        _context.Movement.CurrentDirection = Vector2.zero;

        // Disable all the colliders.
        Collider2D[] colliders = _context.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
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

        // Instantiate the dead particles effect and destroy the agent.
        StartCoroutine(DeadCoroutine());
    }


    // Instantiate the dead particles effect and destroy the agent.
    private IEnumerator DeadCoroutine() {
        // Instantiate the dead particles effect (without changing the z-index).
        Vector3 deadParticlesPosition = new(_context.transform.position.x, _context.transform.position.y, _destroyParticlesEffect.transform.localPosition.z);
        GameObject deadParticles = Instantiate(_destroyParticlesEffect, deadParticlesPosition, Quaternion.identity);

        // Wait for the dead particles effect to finish.
        ParticleSystem.MainModule parts = deadParticles.GetComponent<ParticleSystem>().main;
        parts.startColor = _destroyParticlesEffectColor;
        yield return new WaitForSeconds(parts.duration + parts.startLifetime.constant);

        // Call the Die method of the agent.
        _context.GetComponentInParent<IAgent>().Die();
        yield break;
    }
}