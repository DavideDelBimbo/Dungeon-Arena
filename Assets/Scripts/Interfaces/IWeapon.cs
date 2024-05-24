using UnityEngine;
using static Character;

public interface IWeapon {
    int Damage { get; set; }

    void Attack(FacingDirection facingDirection, Vector2 direction);
    void DealDamage(Collider2D other);
}
