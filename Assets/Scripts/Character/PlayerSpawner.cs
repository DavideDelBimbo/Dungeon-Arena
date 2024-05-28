using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    private void Start() {
        // Spawn the selected character.
        GameManager.Instance.Player = Instantiate(SelectCharacterManager.Instance.SelectedPlayerCharacter, transform.position, Quaternion.identity);

        // Set the player's health to the maximum health.
        GameManager.Instance.Player.Health = GameManager.Instance.MaxHealth;
    }
}
