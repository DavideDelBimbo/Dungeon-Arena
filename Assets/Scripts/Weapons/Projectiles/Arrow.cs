using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons.Projectiles {

    [RequireComponent(typeof(SpriteRenderer))]
    public class Arrow : Projectile {
        [Header("Arrow Settings")]
        [SerializedDictionary("FacingDirection", "Sprite"), SerializeField]
        private SerializedDictionary<FacingDirection, Sprite> _arrowSprites;

        [SerializedDictionary("FacingDirection", "Shadow"), SerializeField]
        private SerializedDictionary<FacingDirection, GameObject> _arrowShadows;

        public override void Fire(FacingDirection facingDirection, Vector2 direction) {
            // Set the arrow sprite and enable the hit box.
            GetComponent<SpriteRenderer>().sprite = _arrowSprites[facingDirection];

            // Set the arrow shadow.
            _arrowShadows.Values.ToList().ForEach(shadow => shadow.SetActive(false));
            _arrowShadows[facingDirection].SetActive(true);

            // Fire the arrow.
            base.Fire(facingDirection, direction);
        }
    }
}