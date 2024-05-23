using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    private enum AllowedDirection { Stop, Up, Down, Left, Right }


    [Header("Movement Settings")]
    [SerializeField] float _speed = 5.0f;
    [SerializeField] AllowedDirection _initialDirection = AllowedDirection.Stop;
    [SerializeField] float _stepSize = 1.0f;
    [SerializeField] LayerMask _obstacleLayer;

    private Transform _movePoint;


    public Vector2 CurrentDirection { get; set; }
    public Rigidbody2D Body { get; private set; }


    void Awake() {
        Body = GetComponent<Rigidbody2D>();
    }

    void Start() {
        CurrentDirection = ConvertAllowedDirectionToVector(_initialDirection);

        // Create a move point object representing the next target position to which the object will move.
        _movePoint = new GameObject("Move Point").transform;
        _movePoint.position = Body.position;
    }

    void FixedUpdate() {
        MoveTowardsTarget();
    }


    // Move the object in the direction of the move point with defined speed and step size (grid-base movement).
    private void MoveTowardsTarget() {
        // Move the object towards the move point.
        Body.MovePosition(Vector2.MoveTowards(Body.position, _movePoint.position, _speed * Time.deltaTime));

        // If the object is close enough to the move point, move the move point to the next position.
        if (Vector2.Distance(Body.position, _movePoint.position) <= Mathf.Epsilon) {
            if (Mathf.Abs(CurrentDirection.x) == 1f) {
                Vector3 targetPosition = _movePoint.position + new Vector3(CurrentDirection.x * _stepSize, 0f, 0f);
                if (!Physics2D.OverlapCircle(targetPosition, _stepSize/2, _obstacleLayer)) {
                    _movePoint.position = targetPosition;
                }
            } else if (Mathf.Abs(CurrentDirection.y) == 1f) {
                Vector3 targetPosition = _movePoint.position + new Vector3(0f, CurrentDirection.y * _stepSize, 0f);
                if (!Physics2D.OverlapCircle(targetPosition, _stepSize/2, _obstacleLayer)) {
                    _movePoint.position = targetPosition;
                }
            }
        }
    }

    // Get the direction vector based on the allowed direction value.
    private Vector2 ConvertAllowedDirectionToVector(AllowedDirection direction) {
        return direction switch {
            AllowedDirection.Stop => Vector2.zero,
            AllowedDirection.Up => Vector2.up,
            AllowedDirection.Down => Vector2.down,
            AllowedDirection.Left => Vector2.left,
            AllowedDirection.Right => Vector2.right,
            _ => throw new InvalidEnumArgumentException($"Invalid allowed direction value: {direction}.")
        };
    }
}
