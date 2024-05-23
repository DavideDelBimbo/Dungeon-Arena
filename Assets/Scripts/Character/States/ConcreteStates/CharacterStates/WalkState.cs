using UnityEngine;

public class WalkState : CharacterState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Get new direction based on input.
        Vector2 newDirection = InputHandler.GetMovement(); 

        // Transition to Walk state if direction changes.
        if (newDirection != _context.Movement.CurrentDirection) {
            _context.Movement.CurrentDirection = newDirection;
            //_context.Movement.TargetPosition = _context.Movement.Body.position + newDirection;
            _context.StateMachine.CurrentFacingDirection = _context.StateMachine.ConvertVectorToFacingDirection(newDirection);

            _context.StateMachine.TransitionToState(_context.WalkState);
        }
        // Transition to Idle state if character stops moving.
        else if (newDirection == Vector2.zero) {
            _context.Movement.CurrentDirection = Vector2.zero;
            _context.StateMachine.TransitionToState(_context.IdleState);
        }
        // Transition to Attack state if fire button is pressed.
        else if (InputHandler.GetFire()) {
            _context.Movement.CurrentDirection = Vector2.zero;
            _context.StateMachine.TransitionToState(_context.AttackState);
        }
    }
}
