using AYellowpaper.SerializedCollections;
using UnityEngine;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons.Projectiles {

    [RequireComponent(typeof(SpriteRenderer))]
    public class Arrow : Projectile {
        [Header("Arrow Settings")]
        [SerializedDictionary("FacingDirection", "Sprite"), SerializeField]
        private SerializedDictionary<FacingDirection, Sprite> _arrowSprites;


        public override void Fire(FacingDirection facingDirection, Vector2 direction) {
            // Set the arrow sprite and enable the hit box.
            GetComponent<SpriteRenderer>().sprite = _arrowSprites[facingDirection];

            // Fire the arrow.
            base.Fire(facingDirection, direction);
        }
    }
}