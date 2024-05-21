using UnityEngine;

public class AIInputHandler : MonoBehaviour, IInputHandler {
    [Header("AI Settings")]
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private Player _target;

    public float DetectionRange { get => _detectionRange; private set => _detectionRange = value; }

    public float AttackRange { get => _attackRange; private set => _attackRange = value; }

    public Player Target { get => _target; private set => _target = value; }

    [Header("Gizmos Settings")]
    [SerializeField] private bool _showGizmos = true;
    [SerializeField] private Color _gizmoDetectionColor = Color.green;
    [SerializeField] private Color _gizmoAttackColor = Color.blue;
    [SerializeField] private Color _gizmoDetectedColor = Color.red;
    
    
    private Vector2 _currentMovement;

    public Vector2 GetMovement() {
        return _currentMovement;
    }

    public void SetMovement(Vector2 movement) {
        _currentMovement = movement;
    }

    public bool GetFire() {
        return false;
    }


    // Gizmos drawing for the AI.
    private void OnDrawGizmos() {
        if (_showGizmos) {
            Gizmos.color = _gizmoDetectionColor;
            if (_target != null && Vector2.Distance(transform.position, _target.transform.position) <= _detectionRange) {
                Gizmos.color = _gizmoDetectedColor;
            }
            Gizmos.DrawWireSphere(transform.position, _detectionRange);

            Gizmos.color = _gizmoAttackColor;
            Gizmos.DrawWireSphere(transform.position, _attackRange);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, _target.transform.position);
        }
    }
}
