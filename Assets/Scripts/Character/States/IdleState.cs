using UnityEngine;

public class IdleState : CharacterState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Transition to walk state if player is moving.
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            character.TransitionToState(State.Walk);
        }
        // Transition to attack state if fire button is pressed.
        else if (Input.GetButtonDown("Fire1")) {
            character.TransitionToState(State.Attack);
        }
    }
}