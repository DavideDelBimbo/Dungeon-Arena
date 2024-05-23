using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AIInputHandler : MonoBehaviour, IInputHandler {
    [Header("AI Settings")]
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    [Header("Gizmos Settings")]
    [SerializeField] private bool _showGizmos = true;
    [SerializeField] private Color _gizmoLineOfSightColor = Color.yellow;
    [SerializeField] private Color _gizmoDetectionColor = Color.green;
    [SerializeField] private Color _gizmoDetectedColor = Color.red;
    [SerializeField] private Color _gizmoAttackColor = Color.blue;

    private IMovementStrategy _movementStrategy;
    private Transform _target;

    public float DetectionRange => _detectionRange;
    public float AttackRange => _attackRange;

    public Enemy Enemy { get; private set; }

    public void SetMovementStrategy(IMovementStrategy movementStrategy) => _movementStrategy = movementStrategy;
    public void SetTarget(Transform target) => _target = target;

    public bool IsPlayerDetected => _target != null && DetectPlayer() != null;


    private void Awake() {
        Enemy = GetComponent<Enemy>();
    }


    public Vector2 GetMovement() {
        if (_movementStrategy != null) {
            return _movementStrategy.GetMovement();
        }
        return Vector2.zero;
    }

    public bool GetFire() {
        /*if (IsTargetDetected && Vector2.Distance(transform.position, _target.position) <= AttackRange) {
            return true;
        }*/
        return false;
    }

    public Transform DetectPlayer() {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, DetectionRange, _playerLayer);

        // Check if the player is in line of sight.
        if (hit != null && HasLineOfSight(hit.transform)) {
            return hit.transform;
        }
        return null;
    }

    private bool HasLineOfSight(Transform target) {
        Vector2 direction = (target.transform.position - Enemy.transform.position).normalized;
        float distance = Vector2.Distance(Enemy.transform.position, target.position);

        // Check if there is an obstacle between the enemy and the target.
        RaycastHit2D hit = Physics2D.Raycast(Enemy.transform.position, direction, distance, _obstacleLayer);
        return hit.collider == null;
    }


    // Debugging Gizmos.
    private void OnDrawGizmos() {
        if (_showGizmos) {

            Gizmos.color = _gizmoDetectionColor;
            if(IsPlayerDetected) {
                Gizmos.color = _gizmoLineOfSightColor;
                Gizmos.DrawLine(transform.position, _target.position);

                Gizmos.color = _gizmoDetectedColor;
            }
            Gizmos.DrawWireSphere(transform.position, DetectionRange);

            Gizmos.color = _gizmoAttackColor;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}
