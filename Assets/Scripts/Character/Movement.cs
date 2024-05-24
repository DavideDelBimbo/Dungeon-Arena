using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    private enum AllowedDirection { Stop, Up, Down, Left, Right }


    [Header("Movement Settings")]
    [SerializeField] float _speed = 5.0f;
    [SerializeField] AllowedDirection _initialDirection = AllowedDirection.Stop;

    private bool isKnockedBack = false;


    public Vector2 CurrentDirection { get; set; }
    public Rigidbody2D Body { get; private set; }


    void Awake() {
        Body = GetComponent<Rigidbody2D>();
    }

    void Start() {
        CurrentDirection = ConvertAllowedDirectionToVector(_initialDirection);
    }

    void FixedUpdate() {
        if (!isKnockedBack) {
            Move();
        }
    }


    // Move the object in the direction of the vector with defined speed from the start position.
    private void Move() {
        // Get the current position of the object and the translation vector.
        Vector2 startPosition = Body.position;
        Vector2 translation = _speed * Time.fixedDeltaTime * CurrentDirection;

        // Move the object to the new position.
        Body.MovePosition(startPosition + translation);
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

    public IEnumerator KnockBack(Vector2 direction, float power, float duration) {
        // Apply knockback force to the character.
        isKnockedBack = true;
        Body.AddForce(direction * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);

        // Stop the knockback force and reset velocity
        Body.velocity = Vector2.zero;
        isKnockedBack = false;
        yield break;
    }
}
