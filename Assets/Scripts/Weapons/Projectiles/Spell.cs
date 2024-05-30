using UnityEngine;
using DungeonArena.Utils;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons.Projectiles {

    [RequireComponent(typeof(AnimatedSprite))]
    public class Spell : Projectile {
        private AnimatedSprite _animatedSprite;

        private void Awake() {
            _animatedSprite = GetComponent<AnimatedSprite>();
        }

        public override void Fire(FacingDirection facingDirection, Vector2 direction) {
            // Calculate the angle of rotation from the direction.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the spell sprite.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Set the spell sprite and enable the hit box.
            _animatedSprite.Play();

            // Fire the spell.
            base.Fire(facingDirection, direction);
        }
    }
}