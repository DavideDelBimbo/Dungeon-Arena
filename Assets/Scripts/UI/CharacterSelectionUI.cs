
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour {
    [Header("UI Settings")]
    [SerializeField] private GameObject _characterButtonPrefab;
    [SerializeField] private Transform _charactersPanel;
    [SerializeField] private Button _startGameButton;

    private void Start() {
        // Disable start button at the beginning.
        _startGameButton.interactable = false;
        
        foreach(GameObject character in CharacterSelectionManager.Instance.CharacterList) {
            GameObject characterButton = Instantiate(_characterButtonPrefab, _charactersPanel.transform);
            characterButton.GetComponentInChildren<SpriteRenderer>().sprite = character.GetComponentInChildren<Character>().PreviewSprite;

            Button button = characterButton.GetComponent<Button>();
            button.onClick.AddListener(() => {
                // Disable the current button.
                button.interactable = false;

                // Enable the previous button.
                if (CharacterSelectionManager.Instance.CurrentCharacterIndex != -1) 
                    _charactersPanel.GetChild(CharacterSelectionManager.Instance.CurrentCharacterIndex).GetComponent<Button>().interactable = true;

                // Select the character.
                CharacterSelectionManager.Instance.SelectCharacter(character);

                // Enable the start game button if a character is selected.
                if (CharacterSelectionManager.Instance.CurrentCharacterIndex != -1)
                    _startGameButton.interactable = true;
            });
        }
    }
}
