using UnityEngine;

[RequireComponent(typeof(AnimatedSprite))]
[RequireComponent(typeof(Collider2D))]
public abstract class DroppableItem : MonoBehaviour, IDroppable {
    [Header("Droppable Item Settings")]
    [SerializeField] private float _lifetime = 5f;
    [SerializeField, Range(0, 1)] private float _dropChance = 0.5f;
    [SerializeField] private GameObject _itemEffect;

    private GameObject _gameObject;


    public float DropChance => _dropChance;


    public void Drop(Vector2 position) {
        _gameObject = Instantiate(gameObject, position, Quaternion.identity);
        _gameObject.GetComponent<AnimatedSprite>().Play();
        Invoke(nameof(DestroyItemAfterLifeTime), _lifetime);
    }

    protected abstract void ApplyEffect(IAgent agent);


    private void OnTriggerEnter2D(Collider2D other) {
        // Check if Player picked up the item.
        if (other.TryGetComponent<Player>(out var player)) {
            ApplyEffect(player);
            Destroy(gameObject);
        }
    }

    private void DestroyItemAfterLifeTime() {
        Destroy(_gameObject);
    }

    private void OnDestroy() {
        if (_itemEffect != null) {
            // Instantiate the hit effect (without changing the z-index).
            Vector3 itemEffectPosition = new(transform.position.x, transform.position.y, _itemEffect.transform.localPosition.z);
            Instantiate(_itemEffect, itemEffectPosition, Quaternion.identity);
        }
    }
}
