using UnityEngine;
using static Character;

public abstract class Weapon : MonoBehaviour, IWeapon {
    [Header("Weapon Settings")]
    [SerializeField] protected int _damage;
    [SerializeField] protected int _knockBackPower;


    public abstract void Attack(FacingDirection facingDirection, Vector2 direction);
}
