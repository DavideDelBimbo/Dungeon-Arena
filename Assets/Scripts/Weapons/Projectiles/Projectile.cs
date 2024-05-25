using System.Collections.Generic;
using UnityEngine;
using static Character;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour {
    [Header("Projectile Settings")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _knockBackPower = 5;
    [SerializeField] protected float _knockBackDuration = 0.1f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] GameObject _hitEffect;

    private Rigidbody2D _rigidbody;
    private Vector2 _knockBackDirection;
    private IAgent _owner;
    private readonly List<Collider2D> _collidersHit = new();


    public IAgent Owner { get => _owner; set => _owner = value; }


    protected void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Start() {
        // Destroy the projectile after a certain amount of time.
        Destroy(gameObject, _lifeTime);
    }


    public virtual void Fire(FacingDirection facingDirection, Vector2 direction) {
        // Set the projectile velocity based on the direction.
        _rigidbody.velocity = direction * _speed;

        // Set the knockback direction.
        _knockBackDirection = direction;
    }


    protected void OnTriggerEnter2D(Collider2D other) {
        // Ignore collision with the owner of the projectile.
        if (_owner != null && other.GetComponentInParent<IAgent>() == _owner) {
            return;
        }

        // Ignore collision with dropable items.
        if (other.GetComponent<IDroppable>() != null) {
            return;
        }

        // Check if the target collider is a hit box.
        HitBox hitBox = other.GetComponent<HitBox>();

        // Deal damage to the target collider (if not already hit during this attack).
        if (hitBox != null && !_collidersHit.Contains(other)) {
            _collidersHit.Add(other);

            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            damageable?.TakeDamage(_damage);
            damageable?.KnockBack(_knockBackDirection, _knockBackPower, _knockBackDuration);
        }

        // Destroy the projectile after hitting an object.
        Destroy(gameObject);
    }


    private void OnDestroy() {
        if (_hitEffect != null) {
            // Instantiate the hit effect (without changing the z-index).
            Vector3 hitEffectPosition = new(transform.position.x, transform.position.y, _hitEffect.transform.localPosition.z);
            Instantiate(_hitEffect, hitEffectPosition, Quaternion.identity);
        }
    }
}
