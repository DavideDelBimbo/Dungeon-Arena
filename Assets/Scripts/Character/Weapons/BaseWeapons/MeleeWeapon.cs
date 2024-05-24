using System;
using UnityEngine;
using static Character;

public abstract class MeleeWeapon : Weapon {
    // Attack with the weapon.
    public override void Attack(FacingDirection facingDirection, Vector2 direction) {
        EnableHitBox(facingDirection);

        // Set the knockback direction.
        _knockBackDirection = direction;
    }

    // Post attack with the weapon.
    public virtual void PostAttack(FacingDirection facingDirection) {
        DisableHitBox(facingDirection);
    }


    // Enable the hit box of the weapon.
    protected abstract void EnableHitBox(FacingDirection facingDirection);

    // Disable the hit box of the weapon.
    protected abstract void DisableHitBox(FacingDirection facingDirection);
}
