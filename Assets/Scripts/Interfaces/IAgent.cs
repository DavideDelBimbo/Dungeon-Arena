using System;
using UnityEngine;

namespace DungeonArena.Interfaces {
    public enum AgentType { Player, Enemy }

    public interface IAgent {
        int Health { get; set; }
        Action<IAgent> OnDeath { get; set; }
        AgentType AgentType { get; }
        Transform Transform { get; }

        void Die();
    }
}