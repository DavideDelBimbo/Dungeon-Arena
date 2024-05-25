using AYellowpaper.SerializedCollections;
using UnityEngine;
using static Character;

[RequireComponent(typeof(SpriteRenderer))]
public class Arrow : Projectile {
    [Header("Arrow Settings")]
    [SerializedDictionary("FacingDirection", "Sprite"), SerializeField]
    private SerializedDictionary<FacingDirection, Sprite> _arrowSprites;

    [SerializedDictionary("FacingDirection", "Hit Box"), SerializeField]
    private SerializedDictionary<FacingDirection, Collider2D> _arrowHitBoxes;


    public override void Fire(FacingDirection facingDirection, Vector2 direction) {
        // Set the arrow sprite and enable the hit box.
        GetComponent<SpriteRenderer>().sprite = _arrowSprites[facingDirection];
        _arrowHitBoxes[facingDirection].enabled = true;

        // Fire the arrow.
        base.Fire(facingDirection, direction);
    }
}