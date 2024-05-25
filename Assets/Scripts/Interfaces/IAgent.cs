using UnityEngine;

public interface IAgent {
    int Health { get; set; }

    void Die();
}