using UnityEngine;
using DungeonArena.Interfaces;
using DungeonArena.Weapons.Projectiles;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons {
    public class Staff : RangedWeapon {
        [Header("Staff Settings")]
        [SerializeField] private Spell _spellPrefab;


        // Fire a spell.
        protected override void FireProjectile(FacingDirection facingDirection, Vector2 direction) {
            // Instantiate the spell.
            Spell spell = Instantiate(_spellPrefab, transform.position, Quaternion.identity);
            spell.Owner = GetComponentInParent<IAgent>();

            // Fire the arrow.
            spell.Fire(facingDirection, direction);
        }
    }
}