using UnityEngine;
using DungeonArena.CharacterControllers;

namespace DungeonArena.Strategies.MovementStrategies {
    public class PatrolMovementStrategy : BaseMovementStrategy {
        public PatrolMovementStrategy(Enemy enemy, float tolerance = 0.1f, float maxDistanceFromPath = 1.5f) : base(enemy, tolerance, maxDistanceFromPath) {}


        public override Vector2 GetMovement() {
            if (ShouldRecalculatePath()) {
                CalculatePathToTarget(_enemy.Spawner.transform.position);
            }

            if (_currentPath.Count > 0) {
                // Move the enemy to the next node in the path.
                return MoveToNextNode();
            }

            return Vector2.zero;
        }


        // Check if the path should be recalculated.
        private bool ShouldRecalculatePath() {
            // Get the distance from the enemy spawn.
            float distanceFromSpawn = Vector2.Distance(_enemy.transform.position, _enemy.Spawner.transform.position);

            // Recalculate the path if the enemy is path is empty and the enemy is far from the spawn.
            return _currentPath != null && _currentPath.Count == 0 && distanceFromSpawn > _enemy.Spawner.SpawnRadius;
        }
    }
}