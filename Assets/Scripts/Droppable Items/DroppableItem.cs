using UnityEngine;

[RequireComponent(typeof(AnimatedSprite))]
[RequireComponent(typeof(Collider2D))]
public abstract class DroppableItem : MonoBehaviour {
    [Header("Droppable Item Settings")]
    [SerializeField] private float _lifetime = 5f;
    [SerializeField, Range(0, 1)] private float _dropChance = 0.5f;
    [SerializeField] private GameObject _itemEffect;

    public float DropChance => _dropChance;

    public static void DropItem(DroppableItem itemPrefab, Vector3 position) {
        // Instantiate the item at the specified position (without changing the z-index).
        Vector3 dropPosition = new(position.x, position.y, itemPrefab.transform.localPosition.z);
        DroppableItem droppedItem = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

        // Drop the item.
        droppedItem.Drop();
    }

    protected abstract void ApplyEffect(IAgent agent);


    private void Drop() {
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
            ApplyEffect(player);
            DestroyItem();
        }
    }

    private void DestroyItem() {
        if (_itemEffect != null) {
            // Instantiate the hit effect (without changing the z-index).
            Vector3 itemEffectPosition = new(transform.position.x, transform.position.y, _itemEffect.transform.localPosition.z);
            Instantiate(_itemEffect, itemEffectPosition, Quaternion.identity);
        }

        // Destroy the item.
        CancelInvoke();
        Destroy(gameObject);
    }
}
