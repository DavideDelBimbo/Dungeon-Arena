using UnityEngine;

public class WalkState : CharacterState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Get new direction based on input.
        Vector2 newDirection = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Avoid diagonal movement.
        if (newDirection.x != 0) {
            newDirection.y = 0;
        }

        // Transition to walk state if direction changes.
        if (newDirection != character.Movement.CurrentDirection) {
            character.Movement.CurrentDirection = newDirection;
            character.CurrentFacingDirection = character.GetFacingDirection(newDirection);

            character.TransitionToState(State.Walk);
        }
        // Transition to idle state if character stops moving.
        else if (newDirection == Vector2.zero) {
            character.Movement.CurrentDirection = Vector2.zero;
            character.TransitionToState(State.Idle);
        }
        // Transition to attack state if fire button is pressed.
        else if (Input.GetButtonDown("Fire1")) {
            character.Movement.CurrentDirection = Vector2.zero;
            character.TransitionToState(State.Attack);
        }
    }
}
