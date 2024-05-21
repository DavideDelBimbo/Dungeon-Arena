using UnityEngine;

public class WaitState : EnemyBaseState {
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

        // Transition to Chase state if player is in range.
        if (Vector2.Distance(_enemy.transform.position, InputHandler.Target.transform.position) <= InputHandler.DetectionRange) {
            _enemy.TransitionToState(EnemyState.Chase);
        }
    }

    private void TransitionToPatrol() {
        _enemy.TransitionToState(EnemyState.Patrol);
    }
}
