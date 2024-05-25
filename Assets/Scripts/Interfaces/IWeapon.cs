using UnityEngine;
using static Character;

public interface IWeapon {
    void Attack(FacingDirection facingDirection, Vector2 direction);
    void PostAttack(FacingDirection facingDirection);
    void DealDamage(Collider2D other);
}
