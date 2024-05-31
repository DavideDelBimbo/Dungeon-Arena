using UnityEngine;
using DungeonArena.Managers;
using DungeonArena.Strategies.MovementStrategies;

namespace DungeonArena.States.EnemyStates {
    public class ChaseState : EnemyState {
        [Header("Chase State Settings")]
        [SerializeField] protected float _recalculatePathDistanceThreshold = 2.0f;


        public override void OnEnter() {
            base.OnEnter();

            // Set the movement strategy.
            if (GameManager.Instance.Player != null)
                InputHandler.MovementStrategy = new ChaseMovementStrategy(_context, GameManager.Instance.Player, _tolerance, _maxDistanceFromPath, _recalculatePathDistanceThreshold);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            // Transition to Wait state if player is not detected.
            if (!InputHandler.IsPlayerDetected) {
                _context.StateMachine.TransitionToState(_context.WaitState);
            }
        }
    }
}