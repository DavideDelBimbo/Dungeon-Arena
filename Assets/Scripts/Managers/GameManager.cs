using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
    [Header("Game Settings")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _score = 0;
    [SerializeField] private int _scoreMultiplier = 1;

    public Player _player;

    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Score { get => _score; set => _score = value; }
    public int ScoreMultiplier { get => _scoreMultiplier; set => _scoreMultiplier = value; }
    public float PowerUpDuration { get; set; } = 0;
    public float PowerUpTimer { get; set; } = 0;
    public Player Player { get => _player; set => _player = value; }

    public UnityEvent OnGameOver;


    private void Start() {
        NewGame();
    }


    public void NewGame() {
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


    public IEnumerator UpdatePowerUpDuration(int multiplier, float duration) {
        // Apply the power up effect.
        ScoreMultiplier = multiplier;
        PowerUpDuration = duration;        
        PowerUpTimer = PowerUpDuration;

        // Wait for the power up duration to end.
        while (PowerUpTimer > 0) {
            PowerUpTimer -= 1;
            yield return new WaitForSeconds(1);
        }

        // Reset the power up effect.
        PowerUpTimer = 0;
        PowerUpDuration = 0;
        ScoreMultiplier = 1;
        yield break;
    }
}
