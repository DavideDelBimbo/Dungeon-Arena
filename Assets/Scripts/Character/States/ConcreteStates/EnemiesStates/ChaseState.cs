using UnityEngine;

public class ChaseState : EnemyState {
    public override void OnEnter() {
        base.OnEnter();

        InputHandler.GizmoDetectionColor = Color.red;
    }

    public override void OnUpdate() {
        base.OnUpdate();

        // Move towards the player converted to unit vector.
        Vector2 direction = (_context.Target.position - _context.transform.position).normalized;
        direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
        InputHandler.SetMovement(direction);


        // Transition to Wait state if player is out of range.
        RaycastHit2D hit = Physics2D.CircleCast(_context.transform.position, InputHandler.DetectionRange, Vector2.zero, 0, _context.TargetLayer);
        if (hit.collider == null) {
            _context.StateMachine.TransitionToState(_context.WaitState);
        }
    }

    public override void OnExit() {
        base.OnExit();

        InputHandler.GizmoDetectionColor = Color.green;
    }
}
