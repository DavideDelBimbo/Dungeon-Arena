using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    private enum AllowedDirection { Stop, Up, Down, Left, Right }


    [Header("Movement Settings")]
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _speedMultiplier = 1f;
    [SerializeField] AllowedDirection _initialDirection = AllowedDirection.Stop;

    private bool isKnockedBack = false;
    private Coroutine _activeSpeedMultiplierCoroutine;


    public float Speed { get => _speed; set => _speed = value; }
    public float SpeedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }

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
        Vector2 translation = _speed * _speedMultiplier * Time.fixedDeltaTime * CurrentDirection;

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


    // Apply knockback force to the object.
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


    // Power up the speed of the agent.
    public void PowerUpSpeed(float speedMultiplier, float duration) {
        // Stop the power-up coroutine if it is already running.
        if (_activeSpeedMultiplierCoroutine != null)
            StopCoroutine(_activeSpeedMultiplierCoroutine);

        // Start the new power-up coroutine.
        _activeSpeedMultiplierCoroutine = StartCoroutine(SpeedMultiplierCoroutine(speedMultiplier, duration));
    }

    private IEnumerator SpeedMultiplierCoroutine(float speedMultiplier, float duration) {
        // Update the speed multiplier value.
        _speedMultiplier = speedMultiplier;

        // Wait for the power-up duration.
        yield return new WaitForSeconds(duration);

        // Reset the speed multiplier value.
        _speedMultiplier = 1f;
        yield break;
    }
}
