using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    [Header("Forbidden Layer")]
    [SerializeField] private LayerMask _forbiddenLayers;

    private readonly Vector2[] _availableDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private List<Vector2> _admissibleDirections = new();
    private Vector2 _previousDirection = Vector2.zero;


    private void Start() {
        // Initialize the list of admissible directions.
        foreach (Vector2 direction in _availableDirections) {
            if (IsAdmissibleDirection(direction)) {
                _admissibleDirections.Add(direction);
            }
        }
    }    


    // Determines the best direction to move towards the target position.
    public Vector2 GetBestDirection(Vector2 targetPosition) {
        Vector2 currentPosition = transform.position;
        Vector2 directionToTarget = targetPosition - currentPosition;

        float bestAngle = Mathf.Infinity;
        Vector2 bestDirection = Vector2.zero;

        foreach (Vector2 direction in _admissibleDirections) {
            float angle = Vector2.Angle(direction, directionToTarget);
            if (angle < bestAngle && direction != _previousDirection) {
                bestAngle = angle;
                bestDirection = direction;
                _previousDirection = bestDirection;
            } else if (direction == -_previousDirection) {
                Debug.Log($"You are going back to {_previousDirection}");
            }
        }

        return bestDirection;
    }

    // Check if target position is at the center of the node.
    public bool IsAtCenter(Vector2 position, float tolerance = 0.1f) {
        return Vector2.Distance(transform.position, position) < tolerance;
    }


    // Determines if the direction is admissible.
    private bool IsAdmissibleDirection(Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f, _forbiddenLayers);
        return hit.collider == null;
    }

    private string BestDirectionString(Vector2 direction) {
        return direction switch {
            Vector2 v when v == Vector2.up => "Up",
            Vector2 v when v == Vector2.down => "Down",
            Vector2 v when v == Vector2.left => "Left",
            Vector2 v when v == Vector2.right => "Right",
            _ => "None"
        };
    }
}
