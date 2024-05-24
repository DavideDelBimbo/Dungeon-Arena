using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    private void Start() {
        // Spawn the selected character.
        Instantiate(GameManager.Instance.SelectedPlayerCharacter, transform.position, Quaternion.identity);
    }
}
