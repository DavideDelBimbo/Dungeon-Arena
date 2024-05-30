using UnityEngine;
using DungeonArena.Strategies.MovementStrategies;

namespace DungeonArena.States.EnemyStates {
    public class WaitState : EnemyState {
        [Header("State Settings")]
        [SerializeField] private float _duration = 2f;


        public override void OnEnter() {
            base.OnEnter();

            // Set the movement strategy.
            InputHandler.MovementStrategy = new WaitMovementStrategy(_context);

            // Invoke the transition to Patrol state after the duration.
            Invoke(nameof(TransitionToPatrol), _duration);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            // Transition to Chase state if player is detected.
            if (InputHandler.IsPlayerDetected) {
                _context.StateMachine.TransitionToState(_context.ChaseState);
            }
        }


        private void TransitionToPatrol() {
            _context.StateMachine.TransitionToState(_context.PatrolState);
        }
    }
}