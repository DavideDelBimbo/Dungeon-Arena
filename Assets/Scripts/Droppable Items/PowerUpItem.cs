using System;
using System.Collections;
using UnityEngine;

public class PowerUpItem : DroppableItem {
    [Header("Power Up Item Settings")]
    [SerializeField] private int _powerUpMultiplier = 2;
    [SerializeField] private float _powerUpDuration = 5f;


    protected override void ApplyEffect(IAgent agent) {
        if (agent is IPowerUpable powerUp) {
            powerUp.PowerUp(_powerUpMultiplier, _powerUpDuration);
        }
    }
}
