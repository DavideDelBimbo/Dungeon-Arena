using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
    [Header("Game Settings")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _score = 0;
    [SerializeField] private int _scoreMultiplier = 1;

    private Player _player;
    private Coroutine _activePowerUpCoroutine;

    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Score { get => _score; set => _score = value; }
    public int ScoreMultiplier { get => _scoreMultiplier; set => _scoreMultiplier = value; }
    public float PowerUpDuration { get; set; } = 0;
    public float PowerUpTimer { get; set; } = 0;
    public Player Player { get => _player; set => _player = value; }

    public UnityEvent OnGameOver;


    public void NewGame() {
        if (SceneManager.Instance.IsLoaded && SelectCharacterManager.Instance.SelectedPlayerCharacter != null) {
            // Instantiate the selected player .
            Player = Instantiate(SelectCharacterManager.Instance.SelectedPlayerCharacter);
            
            // Set the player's health to the maximum health.
            Player.Health = MaxHealth;
        }

        // Reset the score.
        Score = 0;
    }

    public void EndGame() {
        // Invoke the game over event.
        OnGameOver?.Invoke();
    }

    public void AddScore(int points) {
        // Add score to the total score.
        Score += points * ScoreMultiplier;
    }


    // Update the power-up timer.
    public void StartPowerUpTimer(float duration) {
        // Stop the power-up timer coroutine if it is already running.
        if (_activePowerUpCoroutine != null) {
            StopCoroutine(_activePowerUpCoroutine);
        }

        // Start the new power-up timer.
        _activePowerUpCoroutine = StartCoroutine(PowerUpTimerCoroutine(duration));
    }

    private IEnumerator PowerUpTimerCoroutine(float duration) {
        // Set the power-up duration.
        PowerUpDuration = duration;
        PowerUpTimer = duration;

        // Wait for the power-up duration.
        while (PowerUpTimer > 0) {
            yield return new WaitForSeconds(1);
            PowerUpTimer--;
        }

        // Reset the power-up duration.
        PowerUpTimer = 0;
        yield break;
    }
}
