using UnityEngine;
using DungeonArena.Managers;
using DungeonArena.CharacterControllers;
using DungeonArena.Pathfinding;
using System.Collections.Generic;

namespace DungeonArena.Strategies.MovementStrategies {
    public class VulnerableMovementStrategy : BaseMovementStrategy {
        private readonly float _searchRadius = 10.0f;
        private Vector2 _targetPosition;
        private Vector2 _currentPlayerDirection;

        public VulnerableMovementStrategy(Enemy enemy, float tolerance = 0.1f, float maxDistanceFromPath = 1.5f, 
                                          float recalculatePathDistanceThreshold = 2.0f, int maxStepsBeforeRecalculate = 100, float searchRadius = 10.0f) :
                                          base(enemy, tolerance, maxDistanceFromPath, recalculatePathDistanceThreshold, maxStepsBeforeRecalculate) {
            _searchRadius = searchRadius;
        }

        public override Vector2 GetMovement() {
            if (ShouldRecalculatePath()) {
                // Calculate the path to the target position.
                _targetPosition = FindSafeTargetPosition();
                CalculatePathToTarget(_targetPosition);
            }

            if (_currentPath.Count > 0) {
                // Move the enemy to the next node in the path.
                return MoveToNextNode();
            }

            return Vector2.zero;
        }

        // Calculate the target position for escaping from the player.
        private Vector2 FindSafeTargetPosition() {
            // Get the position of the player
            Vector2 playerPosition = GameManager.Instance.Player.transform.position;
            Vector2 enemyPosition = _enemy.transform.position;

            // Get the direction to the player.
            _currentPlayerDirection = (playerPosition - enemyPosition).normalized;

            // Get the cardinal direction.
            if (Mathf.Abs(_currentPlayerDirection.x) > Mathf.Abs(_currentPlayerDirection.y)) {
                _currentPlayerDirection = new Vector2(Mathf.Sign(_currentPlayerDirection.x), 0.0f);
            } else {
                _currentPlayerDirection = new Vector2(0.0f, Mathf.Sign(_currentPlayerDirection.y));
            }

            // Get the escape direction from the player (opposite direction to the player).
            Vector2 escapeDirection = -_currentPlayerDirection;

            // Get all possible directions to escape from the player.
            List<Vector2> directions = new() { escapeDirection, new Vector2(escapeDirection.y, escapeDirection.x), new Vector2(-escapeDirection.y, -escapeDirection.x) };

            // Try to find a suitable node in the opposite direction within a range
            foreach (Vector2 direction in directions) {
                Node targetNode = FindNodeInDirection(enemyPosition, direction, _searchRadius);

                if (targetNode != null && targetNode.IsWalkable) {
                    // Set the target position to the node.
                    return targetNode.WorldCenterPosition;
                }
            }

            // If no suitable node was found, set the target position to the enemy's current position.
            return enemyPosition;
        }

        // Find a node in the specified direction within a range.
        private Node FindNodeInDirection(Vector2 origin, Vector2 direction, float range) {
            for (float i = range; i > 0.0f; i -= GridManager.Instance.CellSize) {
                // Calculate the position.
                Vector2 position = origin + direction * i;

                // Get the node from the position.
                Node node = GridManager.Instance.GetNodeFromWorldPoint(position);

                // Check if the node is walkable.
                if (node != null && node.IsWalkable) {
                    return node;
                }
            }

            return null;
        }

        // Check if the path should be recalculated.
        private bool ShouldRecalculatePath() {
            // Get the distance to the target position.
            float distanceToTargetPosition = Vector2.Distance(_targetPosition, _enemy.transform.position);

            // Get the player direction.
            Vector2 playerDirection = (GameManager.Instance.Player.transform.position - _enemy.transform.position).normalized;

            // Get the cardinal direction.
            if (Mathf.Abs(playerDirection.x) > Mathf.Abs(playerDirection.y)) {
                playerDirection = new Vector2(Mathf.Sign(playerDirection.x), 0.0f);
            } else {
                playerDirection = new Vector2(0.0f, Mathf.Sign(playerDirection.y));
            }

            // Recalculate the path if the enemy has reached the target position or the player direction has changed.
            return _currentPath != null && (_currentPath.Count == 0 || distanceToTargetPosition < _tolerance || _currentPlayerDirection != playerDirection);
        }
    }
}
