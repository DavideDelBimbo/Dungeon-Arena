using UnityEngine;

public class PatrolState : EnemyState {
    public override void OnUpdate() {
        base.OnUpdate();
        
        // Move towards the next waypoint.
        //InputHandler.SetMovement((_enemy.Waypoints[_enemy.CurrentWaypoint].position - _enemy.transform.position).normalized);

        // Transition to Chase state if player is in range.
        RaycastHit2D hit = Physics2D.CircleCast(_context.transform.position, InputHandler.DetectionRange, Vector2.zero, 0, _context.TargetLayer);
        if (hit.collider != null) {
            _context.StateMachine.TransitionToState(_context.ChaseState);
        }
    }
}
