using UnityEngine;
using DungeonArena.CharacterControllers;

namespace DungeonArena.Strategies.MovementStrategies {
    public class PatrolMovementStrategy : BaseMovementStrategy {
        private Vector2 _currentTarget;

        public PatrolMovementStrategy(Enemy enemy, float tolerance = 0.1f, float maxDistanceFromPath = 1.5f, 
                                    float recalculatePathDistanceThreshold = 2.0f, int maxStepsBeforeRecalculate = 100) :
                                    base(enemy, tolerance, maxDistanceFromPath, recalculatePathDistanceThreshold, maxStepsBeforeRecalculate)
        {
            SetNewRandomTarget();
        }

        public override Vector2 GetMovement() {
            if (ShouldRecalculatePath()) {
                SetNewRandomTarget();
                CalculatePathToTarget(_currentTarget);
            }

            if (_currentPath.Count > 0) {
                // Move the enemy to the next node in the path.
                return MoveToNextNode();
            }

            return Vector2.zero;
        }

        // Set a new random target within the spawn radius.
        private void SetNewRandomTarget() {
            Vector2 spawnPosition = _enemy.Spawner.transform.position;
            _currentTarget = spawnPosition + Random.insideUnitCircle * _enemy.Spawner.SpawnRadius;
        }

        // Check if the path should be recalculated.
        private bool ShouldRecalculatePath() {
            float distanceFromTarget = Vector2.Distance(_enemy.transform.position, _currentTarget);
            return _currentPath.Count == 0 || distanceFromTarget < _tolerance;
        }
    }
}
