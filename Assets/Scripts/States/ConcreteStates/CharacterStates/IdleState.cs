using UnityEngine;

namespace DungeonArena.States.CharacterStates {
    public class IdleState : CharacterState {
        public override void OnUpdate() {
            base.OnUpdate();

            // Transition to Walk state if player is moving.
            if (InputHandler.GetMovement() != Vector2.zero) {
                _context.StateMachine.TransitionToState(_context.WalkState);
            }
            // Transition to Attack state if fire button is pressed.
            else if (_context.AttackState.CanAttack && InputHandler.GetFire()) {
                _context.StateMachine.TransitionToState(_context.AttackState);
            }
        }
    }
}