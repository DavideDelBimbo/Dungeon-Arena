using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [Header("Game Settings")]
    [SerializeField] private GameObject _gameOverScreen;

    public void GameOver() {
        // Show the game over screen.
        Invoke(nameof(GameOverScreen), 2f);
    }

    private void GameOverScreen() {
        //_gameOverScreen.SetActive(true);
        Debug.Log("Game Over!");
    }
}
