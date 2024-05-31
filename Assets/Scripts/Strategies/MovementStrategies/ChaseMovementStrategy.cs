using UnityEngine;
using DungeonArena.Managers;
using DungeonArena.CharacterControllers;

namespace DungeonArena.Strategies.MovementStrategies {
    public class ChaseMovementStrategy : BaseMovementStrategy {
        private readonly Player _target;
        private readonly float _recalculatePathDistanceThreshold;
        private Vector2 _lastTargetPosition;


        public ChaseMovementStrategy(Enemy enemy, Player target, float tolerance = 0.1f, float maxDistanceFromPath = 1.5f, float recalculatePathDistanceThreshold = 2.0f) : base(enemy, tolerance, maxDistanceFromPath) {
            _target = target;
            _recalculatePathDistanceThreshold = recalculatePathDistanceThreshold;
            _lastTargetPosition = _target.transform.position;
        }


        public override Vector2 GetMovement() {
            if (ShouldRecalculatePath()) {
                // Calculate the path to the player's current position.
                CalculatePathToTarget(_target.transform.position);
                _lastTargetPosition = _target.transform.position;
            }

            if (_currentPath.Count > 0) {
                // Move the enemy to the next node in the path.
                return MoveToNextNode();
            }

            return Vector2.zero;
        }


        // Check if the path should be recalculated.
        private bool ShouldRecalculatePath() {
            // Return false if the target is on an unwalkable node.
            if (GridManager.Instance.GetNodeFromWorldPoint(_target.transform.position).IsWalkable == false) return false;

            // Get the distance to the player's last position.
            float distanceToLastTargetPosition = Vector2.Distance(_lastTargetPosition, _target.transform.position);

            // Recalculate the path if the enemy has line of sight to the player and the player has moved a certain distance.            
            return _currentPath!= null && HasLineOfSight() && (_currentPath.Count == 0 || distanceToLastTargetPosition > _recalculatePathDistanceThreshold);
        }

        // Check if the enemy has line of sight to the player.
        private bool HasLineOfSight() {
            Vector2 direction = (_target.transform.position - _enemy.transform.position).normalized;
            float distance = Vector2.Distance(_enemy.transform.position, _target.transform.position);

            return !Physics2D.Raycast(_enemy.transform.position, direction, distance, _enemy.ObstacleLayers);
        }
    }
}