using UnityEngine;

public interface IDamageble {
    void TakeDamage(int damage);
    void KnockBack(Vector2 direction, float power, float duration = 0.1f);
}