using UnityEngine;
using System;
using DungeonArena.Utils;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons.Projectiles {

    [RequireComponent(typeof(AnimatedSprite))]
    public class Spell : Projectile {
        private AnimatedSprite[] _animatedSprites;

        private void Awake() {
            _animatedSprites = GetComponentsInChildren<AnimatedSprite>();
        }

        public override void Fire(FacingDirection facingDirection, Vector2 direction) {
            // Calculate the angle of rotation from the direction.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the spell sprite.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Flip the shadow sprite.
            if (direction.x > 0 || direction.y > 0) {
                Vector2 shadowPosition = transform.GetChild(0).localPosition;
                shadowPosition.y *= -1;
                transform.GetChild(0).localPosition = shadowPosition;
            }

            // Set the spell sprite and enable the hit box.
            Array.ForEach(_animatedSprites, animatedSprite => animatedSprite.Play());

            // Fire the spell.
            base.Fire(facingDirection, direction);
        }
    }
}