using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DungeonArena.Managers;
using DungeonArena.CharacterControllers;

namespace DungeonArena.UI {
    public class CharacterSelectionMenu : MonoBehaviour {
        [Header("UI Settings")]
        [SerializeField] private GameObject _characterButtonPrefab;
        [SerializeField] private Transform _charactersPanel;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Transform _loadingScreen;

        private void Start() {
            // Disable start button at the beginning.
            _startGameButton.interactable = false;

            // Hide the loading screen.
            _loadingScreen.gameObject.SetActive(false);
            
            foreach(Player playerCharacter in SelectCharacterManager.Instance.PlayerCharactersList) {
                // Create a character button in panel and set the character preview sprite.
                GameObject characterButton = Instantiate(_characterButtonPrefab, _charactersPanel.transform);
                Image[] images = characterButton.GetComponentsInChildren<Image>();
                images[1].sprite = playerCharacter.GetComponentInChildren<Character>().PreviewSprite;

                Button button = characterButton.GetComponent<Button>();
                button.onClick.AddListener(() => {
                    // Disable the current button.
                    button.interactable = false;

                    // Enable the previous button.
                    if (SelectCharacterManager.Instance.SelectedPlayerCharacter != null) {
                        int previousCharacterIndex = System.Array.IndexOf(SelectCharacterManager.Instance.PlayerCharactersList, SelectCharacterManager.Instance.SelectedPlayerCharacter);
                        _charactersPanel.GetChild(previousCharacterIndex).GetComponent<Button>().interactable = true;
                    }

                    // Select the character.
                    SelectCharacterManager.Instance.SelectedPlayerCharacter = playerCharacter;

                    // Enable the start game button if a character is selected.
                    if (SelectCharacterManager.Instance.SelectedPlayerCharacter != null) _startGameButton.interactable = true;
                });
            }
        }


        public void StartGame() {
            // Load the game scene.
            SceneManager.Instance.LoadNextScene();

            _loadingScreen.gameObject.SetActive(true);
            StartCoroutine(LoadingBar());

            // Start a new game.
            GameManager.Instance.NewGame();
        }


        private IEnumerator LoadingBar() {
            Slider loadingBar = _loadingScreen.GetComponentInChildren<Slider>();

            // Update the loading bar value until the scene is loaded.
            while (!SceneManager.Instance.IsLoaded) {
                loadingBar.value = SceneManager.Instance.Progress;
                yield return null;
            }

            yield break;
        }
    }
}