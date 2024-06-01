using UnityEngine;
using DungeonArena.CharacterControllers;
using DungeonArena.Interfaces;
using DungeonArena.Managers;

namespace DungeonArena.DroppableItems {
    public class ScoreMultiplierEffect : ItemEffect {
        [Header("Score Multiplier Settings")]
        [SerializeField] private int _scoreMultiplier = 2;
        [SerializeField] private float _duration = 10;


        public override void ApplyEffect(IAgent target) {
            // Apply the score multiplier effect to the target.
            if (target is Player player) {
                player.PowerUpScore(_scoreMultiplier, _duration);
                GameManager.Instance.StartPowerUpTimer(_duration);
            }
        }
    }
}