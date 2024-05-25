using AYellowpaper.SerializedCollections;
using UnityEngine;
using static Character;

public class Sword : MeleeWeapon {
    [Header("Sword Settings")]    
    [SerializedDictionary("FacingDirection", "Hit Box"), SerializeField]
    private SerializedDictionary<FacingDirection, WeaponHitBox> _swordHitBoxes;


    // Enable the hit box of the sword based on the facing direction.
    protected override void EnableHitBox(FacingDirection facingDirection) {
        _swordHitBoxes[facingDirection].Enable();
    }

    // Disable the hit box of the sword based on the facing direction.
    protected override void DisableHitBox(FacingDirection facingDirection) {
        _swordHitBoxes[facingDirection].Disable();
    }
}