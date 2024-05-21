using UnityEngine;

public class ChaseState : EnemyBaseState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Move towards the player avoid diagonal movement.
        Vector2 direction = (InputHandler.Target.transform.position - _enemy.transform.position).normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            direction.y = 0;
        } else {
            direction.x = 0;
        }
        
        InputHandler.SetMovement(new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y)));

        // Transition to Wait state if player is out of range.
        if (Vector2.Distance(_enemy.transform.position, InputHandler.Target.transform.position) > InputHandler.DetectionRange) {
            _enemy.TransitionToState(EnemyState.Wait);
        }
    }
}
