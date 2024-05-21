using UnityEngine;

public class WalkState : CharacterBaseState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Get new direction based on input.
        Vector2 newDirection = _character.InputHandler.GetMovement();

        // Transition to Walk state if direction changes.
        if (newDirection != _character.Movement.CurrentDirection) {
            _character.Movement.CurrentDirection = newDirection;
            _character.CurrentFacingDirection = _character.GetFacingDirection(newDirection);

            _character.TransitionToState(CharacterState.Walk);
        }
        // Transition to Idle state if character stops moving.
        else if (newDirection == Vector2.zero) {
            _character.Movement.CurrentDirection = Vector2.zero;
            _character.TransitionToState(CharacterState.Idle);
        }
        // Transition to Attack state if fire button is pressed.
        else if (_character.InputHandler.GetFire()) {
            _character.Movement.CurrentDirection = Vector2.zero;
            _character.TransitionToState(CharacterState.Attack);
        }
    }
}
