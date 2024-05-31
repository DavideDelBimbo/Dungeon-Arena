using System.Collections.Generic;
using UnityEngine;
using DungeonArena.Interfaces;
using DungeonArena.Managers;
using DungeonArena.CharacterControllers;
using DungeonArena.Pathfinding;
using DungeonArena.InputHandlers;

namespace DungeonArena.Strategies.MovementStrategies {
    public abstract class BaseMovementStrategy : IMovementStrategy {
        protected Enemy _enemy;
        protected float _tolerance;
        protected float _maxDistanceFromPath = 1.5f;

        protected PathFinding Pathfinding => ((AIInputHandler)_enemy.InputHandler).PathFinding;
        protected Queue<Node> _currentPath = new();
        protected Node _currentNode;


        public BaseMovementStrategy(Enemy enemy, float tolerance = 0.1f, float maxDistanceFromPath = 1.5f) {
                _enemy = enemy;
                _tolerance = tolerance;
                _maxDistanceFromPath = maxDistanceFromPath;

                // Get the start node of the enemy.
                _currentNode = GridManager.Instance.GetNodeFromWorldPoint(_enemy.transform.position);
        }
        

        public abstract Vector2 GetMovement();


        // Calculate the path to the target.
        protected void CalculatePathToTarget(Vector2 targetPosition) {
            // Get the target node.
            Node targetNode = GridManager.Instance.GetNodeFromWorldPoint(targetPosition);

            // Calculate the path to the target node.
            if (targetNode != null)
                _currentPath = new Queue<Node>(Pathfinding.FindPath(_currentNode.WorldPosition, targetNode.WorldPosition));
        }

        // Move the enemy to the next node in the path.
        protected Vector2 MoveToNextNode() {
            // Check that exists a path.
            if (_currentPath == null || _currentPath.Count == 0) {
                return Vector2.zero;
            }

            // Check if the enemy is stuck or off the path.
            if (IsStuckOrOffPath()) {
                // Update the current node with the current position of the enemy.
                _currentNode = GridManager.Instance.GetNodeFromWorldPoint(_enemy.transform.position);

                // Clear the current path.
                _currentPath.Clear();

                return Vector2.zero;
            }

            // Get the next node in the path.
            Node nextNode = _currentPath.Peek();

            // Calculate the distance from the next node.
            float distanceFromNextNode = Vector2.Distance(_enemy.transform.position, nextNode.WorldCenterPosition);

            // Calculate the direction to the next node.
            Vector2 direction = nextNode.GridPosition - _currentNode.GridPosition;

            // Check if the enemy is close to the next node.
            if (distanceFromNextNode < _tolerance) {
                // Update the current node to the reached node.
                _currentNode = _currentPath.Dequeue();
            }            

            return direction;
        }

        private bool IsStuckOrOffPath() {
            // Check if the enemy is blocked by an obstacle.
            if (Physics2D.Raycast(_enemy.transform.position, _enemy.Movement.CurrentDirection, 0.2f, _enemy.ObstacleLayers)) {
                return true;
            }

            // Check if the enemy is too far from the path.
            float distanceFromPath = Vector2.Distance(_enemy.transform.position, _currentNode.WorldCenterPosition);
            if (distanceFromPath > _maxDistanceFromPath) {
                return true;
            }

            return false;
        }
    }
}