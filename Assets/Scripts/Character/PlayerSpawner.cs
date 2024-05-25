using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    private void Start() {
        // Spawn the selected character (without changing the z-index)
        Vector3 spawnPosition = new(transform.position.x, transform.position.y, SelectCharacterManager.Instance.SelectedPlayerCharacter.transform.localPosition.z);
        Player spawnedPlayer = Instantiate(SelectCharacterManager.Instance.SelectedPlayerCharacter, spawnPosition, Quaternion.identity);
        GameManager.Instance.Player = spawnedPlayer;

        // Set the player health to the max health.
        spawnedPlayer.Health = GameManager.Instance.MaxHealth;
    }

}
