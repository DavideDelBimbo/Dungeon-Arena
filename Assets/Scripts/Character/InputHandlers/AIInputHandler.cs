using UnityEngine;

public class AIInputHandler : MonoBehaviour, IInputHandler {
    [Header("AI Settings")]
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _attackRange = 2f;

    private Vector2 _currentMovement;

    public float DetectionRange { get => _detectionRange; private set => _detectionRange = value; }
    public float AttackRange { get => _attackRange; private set => _attackRange = value; }


    // Debugging settings.
    [Header("Gizmos Settings")]
    [SerializeField] private bool _showGizmos = true;
    [SerializeField] private Color _gizmoDetectionColor = Color.green;
    [SerializeField] private Color _gizmoAttackColor = Color.blue;

    public Color GizmoDetectionColor { get => _gizmoDetectionColor; set => _gizmoDetectionColor = value; }
    public Color GizmoAttackColor { get => _gizmoAttackColor; set => _gizmoAttackColor = value; }


    public Vector2 GetMovement() {
        // Avoid diagonal movement.
        if (_currentMovement.x != 0) {
            _currentMovement.y = 0;
        }

        return _currentMovement;
    }

    public void SetMovement(Vector2 movement) {
        _currentMovement = movement;
    }

    public bool GetFire() {
        return false;
    }


    // Debugging gizmos.
    private void OnDrawGizmos() {
        if (_showGizmos) {
            Gizmos.color = _gizmoDetectionColor;
            Gizmos.DrawWireSphere(transform.position, _detectionRange);

            Gizmos.color = _gizmoAttackColor;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}