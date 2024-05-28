using UnityEngine;
using static Character;

public class Bow : RangedWeapon {
    [Header("Bow Settings")]
    [SerializeField] private Arrow _arrowPrefab;


    // Fire an arrow.
    protected override void FireProjectile(FacingDirection facingDirection, Vector2 direction) {
        // Instantiate the arrow.
        Arrow arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        arrow.Owner = GetComponentInParent<IAgent>();

        // Fire the arrow.
        arrow.Fire(facingDirection, direction);
    }
}