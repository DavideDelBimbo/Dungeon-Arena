using UnityEngine;

public class WaitState : EnemyState {
    [Header("State Settings")]
    [SerializeField] private float _duration = 2f;


    public override void OnEnter() {
        base.OnEnter();
        InputHandler.SetMovement(Vector2.zero);

        // Invoke the transition to Patrol state after the duration.
        Invoke(nameof(TransitionToPatrol), _duration);
    }

    public override void OnUpdate() {
        base.OnUpdate();

        // Transition to Chase state if target is in range.
        RaycastHit2D hit = Physics2D.CircleCast(_context.transform.position, InputHandler.DetectionRange, Vector2.zero, 0, _context.TargetLayer);
        if (hit.collider != null) {
            _context.StateMachine.TransitionToState(_context.ChaseState);
        }
    }

    private void TransitionToPatrol() {
        _context.StateMachine.TransitionToState(_context.PatrolState);
    }
}
