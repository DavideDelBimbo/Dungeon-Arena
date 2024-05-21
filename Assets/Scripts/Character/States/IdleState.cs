using UnityEngine;

public class IdleState : CharacterBaseState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Transition to Walk state if player is moving.
        if (_character.InputHandler.GetMovement() != Vector2.zero) {
            _character.TransitionToState(CharacterState.Walk);
        }
        // Transition to Attack state if fire button is pressed.
        else if (_character.InputHandler.GetFire()) {
            _character.TransitionToState(CharacterState.Attack);
        }
    }
}