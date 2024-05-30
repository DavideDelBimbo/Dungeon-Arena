using UnityEngine;

namespace DungeonArena.States.CharacterStates {
    public class WalkState : CharacterState {
        public override void OnUpdate() {
            base.OnUpdate();

            // Get new direction based on input.
            Vector2 newDirection = InputHandler.GetMovement(); 

            // Mantain a safe distance from other agents.
            if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Agent"))) {
                _context.Movement.CurrentDirection = Vector2.zero;
                _context.StateMachine.TransitionToState(_context.IdleState);
            }

            // Transition to Walk state if direction changes.
            if (newDirection != _context.Movement.CurrentDirection) {
                _context.Movement.CurrentDirection = newDirection;
                _context.StateMachine.TransitionToState(_context.WalkState);
            }
            // Transition to Idle state if character stops moving.
            else if (newDirection == Vector2.zero) {
                _context.Movement.CurrentDirection = Vector2.zero;
                _context.StateMachine.TransitionToState(_context.IdleState);
            }
            // Transition to Attack state if fire button is pressed.
            else if (_context.AttackState.CanAttack && InputHandler.GetFire()) {
                _context.Movement.CurrentDirection = Vector2.zero;
                _context.StateMachine.TransitionToState(_context.AttackState);
            }
        }
    }
}