using System;
using System.Collections.Generic;
using UnityEngine;

/*public class PatrolMovementStrategy : IMovementStrategy {
    private readonly float _patrolRadius;
    private Vector2 _newDirection;
    private Transform _waypoint;
    private Node _currentNode;
    private Enemy _enemy;


    public Node CurrentNode  { get => _currentNode; set => _currentNode = value; }


    public PatrolMovementStrategy(float patrolRadius, Transform waypoint, Enemy enemy) {
        _patrolRadius = patrolRadius;
        _waypoint = waypoint;
        _enemy = enemy;
    }

    

    public Vector2 GetMovement() {
        // Update the direction (inside the Patrol state).
        return _newDirection;
    }

    public void UpdateDirection(Vector2 currentPosition, Vector2 targetPosition) {
        float distance = Vector2.Distance(currentPosition, targetPosition);

        // Check if the target is within the patrol radius.
        if (distance > _patrolRadius) {
            // Move towards the target until it is within the patrol radius.
            //_newDirection = (targetPosition - currentPosition).normalized;
            _newDirection = Vector2.zero;
        } else {
            // Change direction.
            _newDirection = GetRandomDirection(currentPosition, targetPosition);
        }
    }


    private Vector2 GetRandomDirection(Vector2 currentPosition, Vector2 targetPosition) {
        Vector2 direction = (targetPosition - currentPosition).normalized;
        float randomX = UnityEngine.Random.Range(-1f, 1f);
        float randomY = UnityEngine.Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

        // Check if the random direction is within the patrol radius.
        if (Vector2.Distance(currentPosition, targetPosition + randomDirection) <= _patrolRadius) {
            return randomDirection;
        } else {
            // If not, return the default direction.
            return direction;
        }
    }

    private bool IsWithinBounds(Vector2 position, Vector2 targetPosition, float minDistanceFromBounds) {
        float distanceToCenter = Vector2.Distance(position, targetPosition);
        return distanceToCenter <= (_patrolRadius - minDistanceFromBounds);
    }

    public Vector2 GetRandomDirectionWithinBounds(Vector2 enemyPosition, Vector2 targetPosition) {
        Vector2 direction = (targetPosition - enemyPosition).normalized;
        float randomX = UnityEngine.Random.Range(-1f, 1f);
        float randomY = UnityEngine.Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

        // Check if the random direction is within the patrol radius.
        if (Vector2.Distance(context.Position, targetPosition + randomDirection) <= _patrolRadius) {
            return randomDirection;
        } else {
            // If not, return the default direction.
            return direction;
        }

    }
    public Vector2 GetMovement() {
        if (_currentNode != null) {
            return _currentNode.GetBestDirection(_waypoint.position);
        }

        return Vector2.zero;
    }
}*/

public class PatrolMovementStrategy : IMovementStrategy {
    private Transform _enemyTransform;
    private Nodo _currentNode;
    private Grid _grid;
    private AStarPathfinding _pathfinding;
    private Queue<Nodo> _currentPath;
    private Vector2 _targetPosition;

    public PatrolMovementStrategy(Transform enemyTransform, Grid grid) {
        _enemyTransform = enemyTransform;
        _grid = grid;
        _pathfinding = new AStarPathfinding(grid);
    }

    public Vector2 GetMovement() {
        if (_currentPath == null || _currentPath.Count == 0) {
            _currentPath = new Queue<Nodo>(_pathfinding.FindPath(_enemyTransform.position, _targetPosition));
        }

        if (_currentPath != null && _currentPath.Count > 0) {
            Nodo nextNode = _currentPath.Peek();
            Vector2 direction = (nextNode.Position - (Vector2)_enemyTransform.position).normalized;
            if (Vector2.Distance(_enemyTransform.position, nextNode.Position) < 0.1f) {
                _currentPath.Dequeue();
            }
            return direction * Time.deltaTime;
        }

        return Vector2.zero;
    }

    public void SetTargetPosition(Vector2 targetPosition) {
        _targetPosition = targetPosition;
        _currentPath = new Queue<Nodo>(_pathfinding.FindPath(_enemyTransform.position, targetPosition));
        Debug.Log(_currentPath);
    }
}
