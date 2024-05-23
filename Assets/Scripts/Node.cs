using UnityEngine;

public class Node : MonoBehaviour {
    [Header("Node Sprites")]
    [SerializeField] SpriteRenderer _upArrow;
    [SerializeField] SpriteRenderer _downArrow;
    [SerializeField] SpriteRenderer _leftArrow;
    [SerializeField] SpriteRenderer _rightArrow;

    [SerializeField] Transform _target;

    private void Awake() {
        _upArrow.enabled = false;
        _downArrow.enabled = false;
        _leftArrow.enabled = false;
        _rightArrow.enabled = false;
    }
}
