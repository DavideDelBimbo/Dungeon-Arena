using UnityEngine;
using static Character;

public abstract class RangedWeapon : MonoBehaviour, IWeapon {
    public virtual void Attack(FacingDirection facingDirection, Vector2 direction) {
        // Fire a projectile.
        FireProjectile(facingDirection, direction);
    }

    public virtual void PostAttack(FacingDirection facingDirection) { }

    public virtual void DealDamage(Collider2D other) { }


    // Fire a projectile.
    protected abstract void FireProjectile(FacingDirection facingDirection, Vector2 direction);
}