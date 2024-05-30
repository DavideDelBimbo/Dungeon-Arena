using UnityEngine;
using System.Collections;
using DungeonArena.Interfaces;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons {
    public abstract class RangedWeapon : MonoBehaviour, IWeapon {
        public IAgent Agent => GetComponentInParent<IAgent>();


        public virtual void Attack(FacingDirection facingDirection, Vector2 direction) {
            FireProjectile(facingDirection, direction);
        }

        public virtual void PostAttack(FacingDirection facingDirection) { }

        public virtual void DealDamage(Collider2D other) { }


        // Fire a projectile.
        protected abstract void FireProjectile(FacingDirection facingDirection, Vector2 direction);
    }
}