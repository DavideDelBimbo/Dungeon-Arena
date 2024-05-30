using UnityEngine;
using DungeonArena.Interfaces;
using DungeonArena.Managers;

namespace DungeonArena.DroppableItems {
    public class HealEffect : ItemEffect {
        [Header("Heal Settings")]
        [SerializeField] private int _healAmount = 10;


        public override void ApplyEffect(IAgent target) {
            // Heal the target (with a limit of the max health value).
            target.Health = Mathf.Min(target.Health + _healAmount, GameManager.Instance.MaxHealth);
        }
    }
}