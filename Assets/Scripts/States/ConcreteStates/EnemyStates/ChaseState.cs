using UnityEngine;
using DungeonArena.Managers;
using DungeonArena.CharacterControllers;
using DungeonArena.Strategies.MovementStrategies;

namespace DungeonArena.States.EnemyStates {
    public class ChaseState : EnemyState {
        private Player _target;


        protected override void Awake() {
            base.Awake();

            if (GameManager.Instance.Player != null)
                _target = GameManager.Instance.Player;
        }


        public override void OnEnter() {
            base.OnEnter();

            // Set the movement strategy.
            InputHandler.MovementStrategy = new ChaseMovementStrategy(_context, _target, _tolerance, _maxDistanceFromPath, _recalculationDistanceThreshold);
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