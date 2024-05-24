using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [Header("Game Settings")]
    [SerializeField] private Player[] _playerCharacterList;
    [SerializeField] private int _score;
    [SerializeField] private GameObject _gameOverScreen;

    private int _selectedPlayerCharacterIndex = -1;


    public Player[] CharacterList => _playerCharacterList;

    public Player SelectedPlayerCharacter {
        get => _selectedPlayerCharacterIndex != -1 ? _playerCharacterList[_selectedPlayerCharacterIndex] : null;
        set => _selectedPlayerCharacterIndex = value != null ? System.Array.IndexOf(_playerCharacterList, value) : -1;
    }

    public void GameOver() {
        // Show the game over screen.
        Invoke(nameof(GameOverScreen), 2f);
    }

    private void GameOverScreen() {
        //_gameOverScreen.SetActive(true);
        Debug.Log("Game Over!");
    }
}
