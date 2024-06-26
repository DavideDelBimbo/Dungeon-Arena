using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DungeonArena.Interfaces;
using DungeonArena.HitBoxes;
using static DungeonArena.CharacterControllers.Character;

namespace DungeonArena.Weapons.Projectiles {
    public abstract class Projectile : MonoBehaviour {
        [Header("Projectile Settings")]
        [SerializeField] private float _speed = 10f;
        [SerializeField] private Vector2 _direction = new(0.0f, 0.0f);
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _knockBackPower = 5;
        [SerializeField] protected float _knockBackDuration = 0.1f;
        [SerializeField] private float _lifeTime = 2f;
        [SerializeField] private GameObject _hitVFX;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private LayerMask _hitLayerMask;

        private Vector2 _knockBackDirection;
        private IAgent _owner;
        private readonly List<Collider2D> _collidersHit = new();

        public IAgent Owner { get => _owner; set => _owner = value; }


        protected virtual void Start() {
            // Destroy the projectile after a certain amount of time.
            //Destroy(gameObject, _lifeTime);
            Invoke(nameof(DestroyProjectile), _lifeTime);
        }

        protected void Update() {
            // Move the projectile in the direction.
            Vector2 startPosition = transform.position;
            Vector2 newPosition = startPosition + _speed * Time.deltaTime * _direction;
            transform.position = newPosition;

            // Check for collision along the projectile path.
            RaycastHit2D[] hits = Physics2D.LinecastAll(startPosition, newPosition, _hitLayerMask);
            foreach (RaycastHit2D hit in hits) {
                HandleHit(hit.collider);
            }
        }


        public virtual void Fire(FacingDirection facingDirection, Vector2 direction) {
            // Set the projectile velocity based on the direction.
            _direction = direction;

            // Set the knockback direction.
            _knockBackDirection = direction;
        }


        private void HandleHit(Collider2D other) {
            // Ignore collision with the owner of the projectile.
            IAgent agent = other.GetComponentInParent<IAgent>();
            if (_owner != null && agent == _owner) {
                return;
            }

            // Ignore collision with agents of the same type.
            if (agent != null && agent.AgentType == _owner.AgentType) {
                return;
            }

            // Check if the target collider is a hit box.
            CharacterHitBox hitBox = other.GetComponent<CharacterHitBox>();

            // Deal damage to the target collider (if not already hit during this attack).
            if (hitBox != null && !_collidersHit.Contains(other)) {
                _collidersHit.Add(other);

                IDamageable damageable = other.GetComponentInParent<IDamageable>();
                damageable?.TakeDamage(_damage);
                damageable?.KnockBack(_knockBackDirection, _knockBackPower, _knockBackDuration);
            }

            if (_hitVFX != null) {
                // Instantiate the hit VFX on the hit position.
                Vector2 position = other.ClosestPoint(transform.position);
                Instantiate(_hitVFX, position, Quaternion.identity);
            }

            // Destroy the projectile after hitting an object.
            Destroy(gameObject);
        }

        // Destroy the projectile.
        private void DestroyProjectile() {
            // Cancel the destroy projectile invoke.
            CancelInvoke();

            // Destroy the projectile.
            StartCoroutine(FadeAndDestroy());
        }

        // Animate the projectile fade and destroy it.
        private IEnumerator FadeAndDestroy() {
            // Scale down the item.
            Vector3 initialScale = transform.localScale;
            Vector3 targetScale = Vector3.zero;

            float time = 0;
            while (time < _fadeDuration) {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, time / _fadeDuration);
                GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, time / _fadeDuration);
                yield return null;
            }

            // Destroy the item.
            Destroy(gameObject);
        }
    }
}