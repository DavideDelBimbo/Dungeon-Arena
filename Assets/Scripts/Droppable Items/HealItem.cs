using UnityEngine;

public class HealItem : DroppableItem {
    [Header("Health Item Settings")]
    [SerializeField] private int _healthAmount = 1;

    protected override void ApplyEffect(IAgent agent) {
        if (agent is Player player) {
            player.Heal(_healthAmount);
        }
    }
}
