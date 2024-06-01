using UnityEngine;
using DungeonArena.Managers;
using DungeonArena.Strategies.MovementStrategies;

namespace DungeonArena.States.EnemyStates {
    public class VulnerableState : EnemyState {
        [Header("Vulnerable State Settings")]
        [SerializeField] private float _searchRadius = 10.0f;


        public override void OnEnter() {
                base.OnEnter();

                // Set the movement strategy.
                InputHandler.MovementStrategy = new VulnerableMovementStrategy(_context, _tolerance, _maxDistanceFromPath, _recalculatePathDistanceThreshold,
                                                                            _maxStepsBeforeRecalculate, _searchRadius);
            }

        public override void OnUpdate() {
            base.OnUpdate();

            // Transition to the wait state when the player is not detected or power-up effect ends.
            if (!InputHandler.IsPlayerDetected || GameManager.Instance.PowerUpTimer <= 0) {
                _context.StateMachine.TransitionToState(_context.WaitState);
            }
        }
    }
}