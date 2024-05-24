using UnityEngine;
using static Character;

public abstract class RangedWeapon : Weapon {
    public override void Attack(FacingDirection facingDirection, Vector2 direction) {
        FireProjectile(facingDirection, direction);

        // Set the knockback direction.
        _knockBackDirection = direction;
    }


    // Fire a projectile.
    protected abstract void FireProjectile(FacingDirection facingDirection, Vector2 direction);
}