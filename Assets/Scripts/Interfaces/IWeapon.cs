using UnityEngine;
using static Character;

public interface IWeapon {
    void Attack(FacingDirection facingDirection, Vector2 direction);
}
