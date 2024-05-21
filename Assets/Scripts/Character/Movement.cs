using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    private enum PossibleDirection { Stop, Up, Down, Left, Right }

    [Header("Movement Settings")]
    [SerializeField] float _speed = 5.0f;
    [SerializeField] PossibleDirection _initialDirection = PossibleDirection.Stop;

    public Vector2 CurrentDirection { get; set; }
    public Rigidbody2D Body { get; private set; }

    void Awake() {
        Body = GetComponent<Rigidbody2D>();
    }

    void Start() {
        CurrentDirection = GetDirection(_initialDirection);
    }

    void FixedUpdate() {
        Move();
    }

    // Move the object in the direction of the vector with defined speed from the start position.
    private void Move() {
        // Get the current position of the object and the translation vector.
        Vector2 startPosition = Body.position;
        Vector2 translation = _speed * Time.fixedDeltaTime * CurrentDirection;

        // Move the object to the new position.
        Body.MovePosition(startPosition + translation);
    }

    private Vector2 GetDirection(PossibleDirection direction) {
        return direction switch {
            PossibleDirection.Up => Vector2.up,
            PossibleDirection.Down => Vector2.down,
            PossibleDirection.Left => Vector2.left,
            PossibleDirection.Right => Vector2.right,
            _ => Vector2.zero,
        };
    }
}
