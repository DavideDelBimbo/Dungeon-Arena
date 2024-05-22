using UnityEngine;

public class IdleState : CharacterState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Transition to Walk state if player is moving.
        if (InputHandler.GetMovement() != Vector2.zero) {
            _context.StateMachine.TransitionToState(_context.WalkState);
        }
        // Transition to Attack state if fire button is pressed.
        else if (InputHandler.GetFire()) {
            _context.StateMachine.TransitionToState(_context.AttackState);
        }
    }
}