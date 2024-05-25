using UnityEngine;
using static Character;

public class Bow : RangedWeapon {
    [Header("Bow Settings")]
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _arrowSpawnPoint;


    // Fire an arrow.
    protected override void FireProjectile(FacingDirection facingDirection, Vector2 direction) {
        // Instantiate the arrow (without changing the z-index).
        Vector3 arrowPosition = new(_arrowSpawnPoint.position.x, _arrowSpawnPoint.position.y, _arrowPrefab.transform.localPosition.z);
        Arrow arrow = Instantiate(_arrowPrefab, arrowPosition, Quaternion.identity);
        arrow.Owner = GetComponentInParent<IAgent>();

        // Fire the arrow.
        arrow.Fire(facingDirection, direction);
    }
}