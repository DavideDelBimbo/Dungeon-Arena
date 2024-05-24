using AYellowpaper.SerializedCollections;
using UnityEngine;
using static Character;

public class Sword : Weapon {
    [Header("Sword Settings")]    
    [SerializedDictionary("FacingDirection", "Hit Box"), SerializeField] private SerializedDictionary<FacingDirection, WeaponHitBox> _swordHitBoxes;

    private WeaponHitBox _currentSwordHitBox;
    private Vector2 _knockBackDirection;


    public override void Attack(FacingDirection facingDirection, Vector2 direction) {
        // Enable the sword collider.
        _currentSwordHitBox = _swordHitBoxes[facingDirection];
        _currentSwordHitBox.Enable();

        // Set the knockback direction.
        _knockBackDirection = direction;
    }

    public override void PostAttack() {
        // Disable the sword collider.
        _currentSwordHitBox.Disable();
    }

    public override void DealDamage(Collider2D other) {
        IDamageble damageble = other.GetComponentInParent<IDamageble>();
        damageble?.TakeDamage(_damage);
        
        IKnockBack knockBack = other.GetComponentInParent<IKnockBack>();
        knockBack?.KnockBack(_knockBackDirection, _knockBackPower);
    }
}