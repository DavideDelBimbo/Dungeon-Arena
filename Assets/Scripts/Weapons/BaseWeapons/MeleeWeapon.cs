using UnityEngine;
using DungeonArena.Interfaces;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons {
    public abstract class MeleeWeapon : MonoBehaviour, IWeapon {
        [Header("Weapon Settings")]
        [SerializeField] protected int _damage = 1;
        [SerializeField] protected int _knockBackPower = 5;
        [SerializeField] protected float _knockBackDuration = 0.1f;
        
        protected Vector2 _knockBackDirection;

        public IAgent Agent => GetComponentInParent<IAgent>();


        // Attack with the weapon.
        public virtual void Attack(FacingDirection facingDirection, Vector2 direction) {
            // Set the knockback direction and target layer.
            _knockBackDirection = direction;

            // Enable the hit box of the weapon.
            EnableHitBox(facingDirection);
        }

        // Post attack with the weapon.
        public virtual void PostAttack(FacingDirection facingDirection) {
            // Disable the hit box of the weapon.
            DisableHitBox(facingDirection);
        }

        // Deal damage to the target collider.
        public virtual void DealDamage(Collider2D other) {
            // Deal damage to the target collider.
            IDamageable damageble = other.GetComponentInParent<IDamageable>();
            damageble?.TakeDamage(_damage);
            damageble?.KnockBack(_knockBackDirection, _knockBackPower, _knockBackDuration);
        }


        // Enable the hit box of the weapon.
        protected abstract void EnableHitBox(FacingDirection facingDirection);

        // Disable the hit box of the weapon.
        protected abstract void DisableHitBox(FacingDirection facingDirection);
    }
}