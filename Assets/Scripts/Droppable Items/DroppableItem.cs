using System;
using UnityEngine;
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
                Array.ForEach(_itemEffects, effect => effect.ApplyEffect(player));

                // Destroy the item.
                DestroyItem();
            }
        }

        private void DestroyItem() {
            if (_destroyItemVFX != null) {
                // Instantiate the hit VFX.
                Vector2 position = GetComponent<Renderer>().bounds.center;
                Instantiate(_destroyItemVFX, position, Quaternion.identity);
            }

            // Destroy the item.
            CancelInvoke();
            Destroy(gameObject);
        }
    }
}