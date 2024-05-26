using System;

public interface IAgent {
    int Health { get; set; }
    Action<IAgent> OnDeath { get; set; }

    void Die();
}