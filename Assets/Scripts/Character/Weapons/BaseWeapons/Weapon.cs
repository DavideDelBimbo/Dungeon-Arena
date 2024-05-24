using UnityEngine;
using static Character;

public abstract class Weapon : MonoBehaviour, IWeapon {
    [field: SerializeField, Header("Weapon Settings")] public int Damage { get; set; } = 1;
    [SerializeField] protected int _knockBackPower = 5;
    [SerializeField] protected float _knockBackDuration = 0.1f;

    protected Vector2 _knockBackDirection;


    public abstract void Attack(FacingDirection facingDirection, Vector2 direction);
    
    // Deal damage to the target collider and apply knockback.
    public virtual void DealDamage(Collider2D other) {
        IDamageble damageble = other.GetComponentInParent<IDamageble>();
        damageble?.TakeDamage(Damage);
        damageble?.KnockBack(_knockBackDirection, _knockBackPower, _knockBackDuration);
    }
}
