using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponHitBox : MonoBehaviour {
    private Collider2D _collider;


    void Awake() {
        _collider = GetComponent<Collider2D>();
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
        // Deal damage to the Damageable component.
        GetComponentInParent<IWeapon>().DealDamage(other);
    }
}
