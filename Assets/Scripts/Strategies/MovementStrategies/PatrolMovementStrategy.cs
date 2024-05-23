using System;
using UnityEngine;

public class PatrolMovementStrategy : IMovementStrategy {
    private readonly float _patrolRadius;
    private Vector2 _newDirection;

    public PatrolMovementStrategy(float patrolRadius) {
        _patrolRadius = patrolRadius;
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

    /*private bool IsWithinBounds(Vector2 position, Vector2 targetPosition, float minDistanceFromBounds) {
        float distanceToCenter = Vector2.Distance(position, targetPosition);
        return distanceToCenter <= (_patrolRadius - minDistanceFromBounds);
    }*/

    /*public Vector2 GetRandomDirectionWithinBounds(Vector2 enemyPosition, Vector2 targetPosition) {
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

    }*/
}