
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMenu : MonoBehaviour {
    [Header("UI Settings")]
    [SerializeField] private GameObject _characterButtonPrefab;
    [SerializeField] private Transform _charactersPanel;
    [SerializeField] private Button _startGameButton;

    private void Start() {
        // Reset the game.
        GameManager.Instance.NewGame();

        // Disable start button at the beginning.
        _startGameButton.interactable = false;
        
        foreach(Player playerCharacter in SelectCharacterManager.Instance.PlayerCharactersList) {
            // Create a character button in panel and set the character preview sprite.
            GameObject characterButton = Instantiate(_characterButtonPrefab, _charactersPanel.transform);
            characterButton.GetComponentInChildren<SpriteRenderer>().sprite = playerCharacter.GetComponentInChildren<Character>().PreviewSprite;

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
}
