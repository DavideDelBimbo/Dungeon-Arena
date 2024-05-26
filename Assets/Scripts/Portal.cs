using UnityEngine;

public class Portal : MonoBehaviour {
    [Header("Portal Settings")]
    [SerializeField] private Transform _destination;

    private void OnTriggerEnter2D(Collider2D other) {
        HitBox hitBox = other.GetComponent<HitBox>();
        Player player = other.GetComponentInParent<Player>();

        // Check if Player entered the portal.
        if (hitBox != null && player != null) {
            // Teleport the player to the destination.
            Vector3 newPosition = player.transform.position;
            newPosition.x = _destination.position.x;
            newPosition.y = _destination.position.y;
            player.transform.position = newPosition;

            // Set the facing direction of the player.
            player.Character.StateMachine.CurrentFacingDirection = Character.FacingDirection.Down;
        }
    }
}
