using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    private void Start() {
        // Spawn the selected character.
        GameObject selectedCharacter = CharacterSelectionManager.Instance.CharacterList[CharacterSelectionManager.Instance.CurrentCharacterIndex];
        Instantiate(selectedCharacter, transform.position, Quaternion.identity);
    }
}
