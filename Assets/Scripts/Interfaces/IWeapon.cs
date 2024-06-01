using UnityEngine;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Interfaces {
    public interface IWeapon {
        IAgent Agent { get; }

        void Attack(FacingDirection facingDirection, Vector2 direction);
        void PostAttack(FacingDirection facingDirection);
        void DealDamage(Collider2D other);
    }
}