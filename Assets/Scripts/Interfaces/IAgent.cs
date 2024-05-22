public interface IAgent {
    int Health { get; set; }

    void TakeDamage(int damage);
    void Die();
}