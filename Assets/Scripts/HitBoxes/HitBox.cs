using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class HitBox : MonoBehaviour {
    protected Collider2D _collider;

    protected virtual void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    public virtual void Enable() {
        _collider.enabled = true;
    }

    public virtual void Disable() {
        _collider.enabled = false;
    }
}
