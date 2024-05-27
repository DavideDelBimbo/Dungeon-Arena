using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(IInputHandler))]
public class Player : MonoBehaviour, IAgent, IDamageable {
    [Header("Player Settings")]
    [SerializeField] private Color _damageFlashColor = Color.red;
    [SerializeField] private float _damageFlashDuration = 0.1f;


    private Coroutine _activeScoreMultiplierCoroutine;


    public int Health { get; set; }
    public Action<IAgent> OnDeath { get; set; }

    public Character Character { get; private set;}
    public Movement Movement { get; private set; }
    public IInputHandler InputHandler { get; private set; }


    private void Awake() {
        Character = GetComponentInChildren<Character>();
        Movement = GetComponent<Movement>();
        InputHandler = GetComponent<IInputHandler>();

        // Dependency injection for the Character.
        Character.Agent = this;
        Character.Movement = Movement;
        Character.InputHandler = InputHandler;
    }


    public void TakeDamage(int damage) {
        // Flash the character when taking damage.
        StartCoroutine(Character.Flash(_damageFlashColor, _damageFlashDuration));

        // Reduce the health of the character.
        Health -= damage;

        if (Health <= 0) {
            // Transition to the dead state.
            Character.StateMachine.TransitionToState(Character.DeadState);
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration = 0.1f) {
        // Apply knockback force to the character.
        StartCoroutine(Movement.KnockBack(direction, power, duration));
    }

    public void Die() {
        // Destroy the player.
        Destroy(gameObject);

        // Show the game over screen.
        //Invoke(nameof(GameManager.Instance.EndGame), 1f);
    }


    // Power-up the score multiplier.
    public void PowerUpScore(int scoreMultiplier, float duration) {
        // Stop the score multiplier coroutine if it is already running.
        if (_activeScoreMultiplierCoroutine != null) {
            StopCoroutine(_activeScoreMultiplierCoroutine);
        }

        // Start the new score multiplier coroutine.
        _activeScoreMultiplierCoroutine = StartCoroutine(ScoreMultiplierCoroutine(scoreMultiplier, duration));
    }

    private IEnumerator ScoreMultiplierCoroutine(int scoreMultiplier, float duration) {
        // Set the score multiplier.
        GameManager.Instance.ScoreMultiplier = scoreMultiplier;

        // Wait for the power-up duration.
        yield return new WaitForSeconds(duration);

        // Reset the score multiplier.
        GameManager.Instance.ScoreMultiplier = 1;
        yield break;
    }
}
