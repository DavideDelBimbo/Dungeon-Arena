using UnityEngine;
using DungeonArena.Interfaces;
using DungeonArena.Managers;
using DungeonArena.CharacterControllers;

namespace DungeonArena.DroppableItems {
    public class SpeedMultiplierEffect : ItemEffect {
        [Header("Speed Multiplier Settings")]
        [SerializeField] private float _speedMultiplier = 1.5f;
        [SerializeField] private float _duration = 10;


        public override void ApplyEffect(IAgent target) {
            // Apply the speed multiplier effect to the target.
            if (target is Player player) {
                player.Movement.PowerUpSpeed(_speedMultiplier, _duration);
                GameManager.Instance.StartPowerUpTimer(_duration);
            }
        }
    }
}