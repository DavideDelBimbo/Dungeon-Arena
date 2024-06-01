using System;
using UnityEngine;
using System.Collections;
using DungeonArena.CharacterControllers;
using DungeonArena.Utils;
using DungeonArena.HitBoxes;

namespace DungeonArena.DroppableItems {

    [RequireComponent(typeof(AnimatedSprite))]
    [RequireComponent(typeof(Collider2D))]
    public class DroppableItem : MonoBehaviour {
        [Header("Droppable Item Settings")]
        [SerializeField] private ItemEffect[] _itemEffects;
        [SerializeField] private float _lifetime = 5f;
        [SerializeField, Range(0, 1)] private float _dropChance = 0.5f;
        [SerializeField] private GameObject _destroyItemVFX;
        [SerializeField] float _scaleDuration = 0.5f;

        private Player _player = null;


        public float DropChance => _dropChance;


        private void Start() {
            // Play the animation of the item.
            GetComponent<AnimatedSprite>().Play();

            // Set the lifetime of the item.
            Invoke(nameof(DestroyItem), _lifetime);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            HitBox hitBox = other.GetComponent<HitBox>();
            Player player = other.GetComponentInParent<Player>();

            // Check if Player picked up the item.
            if (hitBox != null && player != null) {
                // Apply the item effect to the player.
                _player = player;
                Array.ForEach(_itemEffects, effect => effect.ApplyEffect(player));

                // Destroy the item.
                DestroyItem();
            }
        }


        // Destroy the item.
        private void DestroyItem() {
            // Cancel the destroy item invoke.
            CancelInvoke();

            if (_destroyItemVFX != null && _player != null) {
                // Instantiate the hit VFX on the player.
                Vector2 position = _player.transform.GetComponentInChildren<Renderer>().bounds.center;
                GameObject itemEffect = Instantiate(_destroyItemVFX, position, Quaternion.identity, _player.transform);

                // Destroy the item VFX after the animation ends.
                ParticleSystem.MainModule parts = itemEffect.GetComponent<ParticleSystem>().main;
                Destroy(itemEffect, parts.duration + parts.startLifetime.constant);

                // Destroy the item.
                Destroy(gameObject);
            } else {
                // Destroy the item.
                StartCoroutine(ScaleAndDestroy());
            }
        }

        // Animate the item scale and destroy it.
        private IEnumerator ScaleAndDestroy() {
            // Scale down the item.
            Vector3 initialScale = transform.localScale;
            Vector3 targetScale = Vector3.zero;

            float time = 0;
            while (time < _scaleDuration) {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, time / _scaleDuration);
                yield return null;
            }

            // Destroy the item.
            Destroy(gameObject);
        }
    }
}