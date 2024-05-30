using System.Collections.Generic;
using UnityEngine;
using DungeonArena.Interfaces;

namespace DungeonArena.HitBoxes {
    public class WeaponHitBox : HitBox {
        private IWeapon _weapon;
        private readonly List<Collider2D> _collidersHit = new();


        protected override void Awake() {
            base.Awake();
            _weapon = GetComponentInParent<IWeapon>();
        }

        protected void Start() {
            Disable();
        }


        public override void Enable() {
            base.Enable();
            
            // Clear the list of hitted colliders during this attack.
            _collidersHit.Clear();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            // Check if the target collider is a hit box.
            CharacterHitBox hitBox = other.GetComponent<CharacterHitBox>();

            // Deal damage to the target collider (if not already hit during this attack).
            if (hitBox != null && !_collidersHit.Contains(other) && hitBox.Agent.AgentType != _weapon.Agent.AgentType) {
                _collidersHit.Add(other);
                _weapon?.DealDamage(other);
            }
        }
    }
}