using UnityEngine;
using static Character;

public abstract class MeleeWeapon : Weapon {
    // Enable the hit box of the weapon.
    public abstract void EnableHitBox(FacingDirection facingDirection);

    // Disable the hit box of the weapon.
    public abstract void DisableHitBox();

    // Deal damage to the target.
    public abstract void DealDamage(Collider2D other);

    // Attack with the weapon.
    public override void Attack(FacingDirection facingDirection, Vector2 direction) {
        EnableHitBox(facingDirection);
    }
}
