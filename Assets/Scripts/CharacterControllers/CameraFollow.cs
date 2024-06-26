using UnityEngine;
using DungeonArena.Managers;

namespace DungeonArena.CharacterControllers {
    public class CameraFollow : MonoBehaviour {
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector3 _offset;


        private void FixedUpdate() {
            if (GameManager.Instance.Player == null) return;

            Vector3 desiredPosition = GameManager.Instance.Player.transform.position + _offset;
            desiredPosition.z = transform.position.z;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }

    }
}