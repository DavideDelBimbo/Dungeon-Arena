using UnityEngine;
using DungeonArena.CharacterControllers;
using DungeonArena.HitBoxes;

/*namespace DungeonArena.Objects {
    [RequireComponent(typeof(Collider2D))]
    public class Portal : MonoBehaviour {
        [Header("Portal Settings")]
        [SerializeField] private Transform _destination;

        private void OnTriggerEnter2D(Collider2D other) {
            CharacterHitBox hitBox = other.GetComponent<CharacterHitBox>();
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
}*/

using System.Collections;
using System.Collections.Generic;

namespace DungeonArena.Objects {
    [RequireComponent(typeof(Collider2D))]
    public class Portal : MonoBehaviour {
        [Header("Portal Settings")]
        [SerializeField] private Transform _destination;
        [SerializeField] private float _closeDuration = 5.0f;
        [SerializeField] private GameObject _portalDoor;
        [SerializeField] private GameObject _portalLight;

        private static readonly List<Portal> _allPortals = new();
        private bool _isClosed = false;
        private Collider2D _portalCollider;

        private void Awake() {
            _portalCollider = GetComponent<Collider2D>();
            _allPortals.Add(this);
        }

        private void Start() {
            _portalDoor.SetActive(false);
            _portalLight.SetActive(true);
        }

        private void OnDestroy() {
            _allPortals.Remove(this);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (_isClosed) return;

            CharacterHitBox hitBox = other.GetComponent<CharacterHitBox>();
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

                // Start the coroutine to close all portals
                StartCoroutine(CloseAllPortalsTemporarily());
            }
        }

        private IEnumerator CloseAllPortalsTemporarily() {
            // Close all portals
            foreach (Portal portal in _allPortals) {
                portal._isClosed = true;
                portal._portalCollider.enabled = false;
                portal._portalDoor.SetActive(true);
                portal._portalLight.SetActive(false);
            }

            // Wait for the specified duration
            yield return new WaitForSeconds(_closeDuration);

            // Reopen all portals
            foreach (Portal portal in _allPortals) {
                portal._isClosed = false;
                portal._portalCollider.enabled = true;
                portal._portalDoor.SetActive(false);
                portal._portalLight.SetActive(true);
            }
        }
    }
}
