using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DungeonArena.Interfaces;
using DungeonArena.Managers;
using DungeonArena.Utils;

namespace DungeonArena.InputHandlers {
    public class KeyboardInputHandler : MonoBehaviour, IInputHandler {
        [Header("Movement Inpur Settings")]
        [SerializeField] private float _separationDistance = 0.5f;

        public Vector2 GetMovement() {
            Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Avoid diagonal movement.
            if (input.x != 0) {
                input.y = 0;
            }

            // Check if the player has sufficient space to move.
            List<IAgent> agentsInWay = new();
            agentsInWay.AddRange(GameManager.Instance.Enemies);
            if (!MovementUtils.HasSufficientSpace(input, GameManager.Instance.Player, agentsInWay, _separationDistance)) {
                return Vector2.zero;
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