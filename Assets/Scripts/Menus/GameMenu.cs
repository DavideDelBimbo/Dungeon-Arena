using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DungeonArena.Interfaces;
using DungeonArena.Managers;

namespace DungeonArena.UI {
    public class GameMenu : MonoBehaviour {
        [Header("UI Settings")]
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _powerUpSlider;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Transform _settingsMenu;
        [SerializeField] private Transform _gameOverMenu;

        private bool _isSettingsMenuOpen;


        private void Start() {
            // Set the health slider values.
            if (GameManager.Instance.Player != null) {
                _healthSlider.maxValue = GameManager.Instance.MaxHealth;
                _healthSlider.value = GameManager.Instance.Player.Health;
            }

            // Set the power up slider values.
            _powerUpSlider.value = 0;

            // Set the score text.
            _scoreText.text = GameManager.Instance.Score.ToString();

            // Hide the settings menu and game over menu.
            _settingsMenu.gameObject.SetActive(false);
            _gameOverMenu.gameObject.SetActive(false);

            // Subscribe to the game over event.
            if (GameManager.Instance.Player != null) {
                GameManager.Instance.Player.OnDeath += ShowGameOverMenu;
            }
        }

        private void Update() {
            if (GameManager.Instance.Player != null) {
                // Update the health slider value.
                _healthSlider.value = GameManager.Instance.Player.Health;
            }

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
            // End the game.
            GameManager.Instance.EndGame();

            // Reset the time scale.
            Time.timeScale = 1;

            // Reset character selection.
            SelectCharacterManager.Instance.SelectedPlayerCharacter = null;

            // Load the main menu scene.
            SceneManager.Instance.LoadScene(sceneName);
        }

        public void ReturnToScene(int sceneIndex) {
            // End the game.
            GameManager.Instance.EndGame();

            // Reset the time scale.
            Time.timeScale = 1;

            // Reset character selection.
            SelectCharacterManager.Instance.SelectedPlayerCharacter = null;

            // Load the main menu scene.
            SceneManager.Instance.LoadScene(sceneIndex);
        }

        public void QuitGame() {
            SceneManager.QuitGame();
        }


        private void ShowGameOverMenu(IAgent player) {
            // Set the score text in the game over menu and show it.
            _gameOverMenu.GetComponentsInChildren<TextMeshProUGUI>()[2].text = GameManager.Instance.Score.ToString();
            _gameOverMenu.gameObject.SetActive(true);

            // Unsubscribe from the game over event.
            player.OnDeath -= ShowGameOverMenu;

            // End the game.
            GameManager.Instance.EndGame();

            // Pause the game.
            Time.timeScale = 0;
        }
    }
}