using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    private void Start() {
        // Spawn the selected character (without changing the z-index)
        GameManager.Instance.Player = Instantiate(SelectCharacterManager.Instance.SelectedPlayerCharacter, transform.position, Quaternion.identity);
        Vector3 spawnPosition = new(transform.position.x, transform.position.y, GameManager.Instance.Player.transform.localPosition.z);
        GameManager.Instance.Player.transform.position = spawnPosition;

        // Set the player's health to the maximum health.
        GameManager.Instance.Player.Health = GameManager.Instance.MaxHealth;
    }
}
