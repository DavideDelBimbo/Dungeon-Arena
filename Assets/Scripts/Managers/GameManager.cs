using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
    [Header("Game Settings")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _score = 0;

    public Player _player;

    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Score { get => _score; set => _score = value; }
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

    public void AddScore(int score) {
        Score += score;
    }
}
