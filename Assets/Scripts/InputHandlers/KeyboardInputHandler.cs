using UnityEngine;
using UnityEngine.EventSystems;
using DungeonArena.Interfaces;

namespace DungeonArena.InputHandlers {
    public class KeyboardInputHandler : MonoBehaviour, IInputHandler {
        public Vector2 GetMovement() {
            Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Avoid diagonal movement.
            if (input.x != 0) {
                input.y = 0;
            }

            return input;
        }

        public bool GetFire() {
            // Check if the mouse is over a UI element.
            if (EventSystem.current.IsPointerOverGameObject()) {
                return false;
            }

            // Check if the player pressed the fire button.
            return Input.GetButtonDown("Fire1");
        }
    }
}