using AYellowpaper.SerializedCollections;
using UnityEngine;
using static Character;

public class Bow : RangedWeapon {
    [Header("Bow Settings")]
    [SerializeField] private GameObject _arrowPrefab;
    [SerializedDictionary("FacingDirection", "Sprite"), SerializeField] private SerializedDictionary<FacingDirection, Sprite> _arrowSprites;
    [SerializeField] private Transform _arrowSpawnPoint;


    // Fire an arrow.
    protected override void FireProjectile(FacingDirection facingDirection, Vector2 direction) {
        _arrowPrefab.GetComponent<SpriteRenderer>().sprite = _arrowSprites[facingDirection];
        GameObject arrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = direction * 10f;
    }
}