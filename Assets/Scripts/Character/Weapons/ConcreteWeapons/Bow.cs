using AYellowpaper.SerializedCollections;
using UnityEngine;
using static Character;

public class Bow : Weapon {
    [Header("Bow Settings")]
    [SerializeField] private GameObject _arrowPrefab;
    [SerializedDictionary("FacingDirection", "Sprite"), SerializeField] private SerializedDictionary<FacingDirection, Sprite> _arrowSprites;
    [SerializeField] private Transform _arrowSpawnPoint;


    public override void Attack(FacingDirection facingDirection, Vector2 direction) {
        _arrowPrefab.GetComponent<SpriteRenderer>().sprite = _arrowSprites[facingDirection];
        GameObject arrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = direction * 10f;
    }

    public override void PostAttack() {
        // Do nothing.
    }

    public override void DealDamage(Collider2D other) {
        // Do nothing.
    }
}