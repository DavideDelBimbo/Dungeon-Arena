using UnityEngine;
using DungeonArena.Strategies.MovementStrategies;
using DungeonArena.Managers;

namespace DungeonArena.States.EnemyStates {
    public class WaitState : EnemyState {
        [Header("Wait State Settings")]
        [SerializeField] private float _duration = 2.0f;


        public override void OnEnter() {
            base.OnEnter();

            // Set the movement strategy.
            InputHandler.MovementStrategy = new WaitMovementStrategy(_context, _tolerance, _maxDistanceFromPath,
                                                                    _recalculatePathDistanceThreshold, _maxStepsBeforeRecalculate);

            // Invoke the transition to Patrol state after the duration.
            Invoke(nameof(TransitionToPatrol), _duration);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            // Transition to the vulnerable state if the power-up effect is active.
            if (InputHandler.IsPlayerDetected && GameManager.Instance.PowerUpTimer > 0) {
                _context.StateMachine.TransitionToState(_context.VulnerableState);
            }

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