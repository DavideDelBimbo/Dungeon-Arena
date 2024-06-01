using DungeonArena.Managers;
using DungeonArena.Strategies.MovementStrategies;

namespace DungeonArena.States.EnemyStates {
    public class PatrolState : EnemyState {
        public override void OnEnter() {
            base.OnEnter();

            // Set the movement strategy.
            InputHandler.MovementStrategy = new PatrolMovementStrategy(_context, _tolerance, _maxDistanceFromPath,
                                                                        _recalculatePathDistanceThreshold, _maxStepsBeforeRecalculate);
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
    }
}