using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponHitBox : MonoBehaviour {
    private Collider2D _collider;
    private IWeapon _weapon;


    void Awake() {
        _collider = GetComponent<Collider2D>();
        _weapon = GetComponentInParent<IWeapon>();
    }

    void Start() {
        Disable();
    }

    public void Enable() {
        _collider.enabled = true;
    }

    public void Disable() {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _weapon?.DealDamage(other);
    }
}
