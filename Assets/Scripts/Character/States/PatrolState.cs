using UnityEngine;

public class PatrolState : EnemyBaseState {
    public override void OnUpdate() {
        base.OnUpdate();
        
        // Move towards the next waypoint.
        //InputHandler.SetMovement((_enemy.Waypoints[_enemy.CurrentWaypoint].position - _enemy.transform.position).normalized);

        // Transition to Chase state if player is in range.
        if (Vector2.Distance(_enemy.transform.position, InputHandler.Target.transform.position) <= InputHandler.DetectionRange) {
            _enemy.TransitionToState(EnemyState.Chase);
        }
    }
}
