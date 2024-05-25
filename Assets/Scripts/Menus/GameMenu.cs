using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
    [Header("UI Settings")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _powerUpSlider;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _settingsMenu;
    [SerializeField] private Transform _gameOverMenu;

    private bool _isSettingsMenuOpen;
    private bool _isStartPowerUpTimer;


    private void Start() {
        // Set the health slider values.
        _healthSlider.maxValue = GameManager.Instance.MaxHealth;
        _healthSlider.value = GameManager.Instance.Player.Health;

        // Set the power up slider values.
        _powerUpSlider.value = 0;

        // Set the score text.
        _scoreText.text = GameManager.Instance.Score.ToString();

        // Hide the settings menu and game over menu.
        _settingsMenu.gameObject.SetActive(false);
        _gameOverMenu.gameObject.SetActive(false);

        // Subscribe to the game over event.
        GameManager.Instance.OnGameOver.AddListener(ShowGameOverMenu);
    }

    private void Update() {
        // Update the health slider value.
        _healthSlider.value = GameManager.Instance.Player.Health;

        // Update the power up slider value.
         _powerUpSlider.maxValue = GameManager.Instance.PowerUpDuration;
        _powerUpSlider.value = GameManager.Instance.PowerUpTimer;

        // Update the score text.
        _scoreText.text = GameManager.Instance.Score.ToString();

        // Toggle the settings menu by pressing the escape key.
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!_isSettingsMenuOpen)
                PauseGame();
            else
                ResumeGame();
        }
    }

    private void OnDestroy() {
        // Unsubscribe from the game over event.
        GameManager.Instance.OnGameOver.RemoveListener(ShowGameOverMenu);
    }


    public void PauseGame() {
        // Show the settings menu.
        _settingsMenu.gameObject.SetActive(true);
        _isSettingsMenuOpen = true;

        // Pause the game.
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        // Hide the settings menu.
        _settingsMenu.gameObject.SetActive(false);
        _isSettingsMenuOpen = false;

        // Resume the game.
        Time.timeScale = 1;
    }

    public void ReturnToScene(string sceneName) {
        // Reset the time scale.
        Time.timeScale = 1;

        // Reset character selection.
        SelectCharacterManager.Instance.SelectedPlayerCharacter = null;

        // Load the main menu scene.
        SceneManager.LoadScene(sceneName);
    }


    private void ShowGameOverMenu() {
        _gameOverMenu.gameObject.SetActive(true);
    }
}