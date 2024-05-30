using UnityEngine;
using System;
using DungeonArena.Interfaces;
using DungeonArena.CharacterControllers;
using DungeonArena.Pathfinding;
using DungeonArena.Managers;

namespace DungeonArena.InputHandlers {

    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(PathFinding))]
    public class AIInputHandler : MonoBehaviour, IInputHandler {
        [Header("AI Settings")]
        [SerializeField] private float _detectionRange = 5f;
        [SerializeField] private float _attackRange = 2f;
        [SerializeField, Range(0.1f, 10f)] private float _attackSpeed = 1.0f;
        [SerializeField, Range(0.0f, 0.5f)] private float _alignmentDirectionThreshold = 0.1f;
        [SerializeField] private LayerMask _playerLayer;

        [Header("Gizmos Settings")]
        [SerializeField] private bool _showGizmos = true;
        [SerializeField] private Color _gizmoDetectionColor = Color.green;
        [SerializeField] private Color _gizmoDetectedColor = Color.red;
        [SerializeField] private Color _gizmoAttackColor = Color.blue;

        private Enemy _enemy;
        private PathFinding _pathFinding;
        private IMovementStrategy _movementStrategy;
        private Player _target;


        public float DetectionRange => _detectionRange;
        public float AttackRange => _attackRange;

        public Enemy Enemy { get => _enemy; private set => _enemy = value; }
        public PathFinding PathFinding { get => _pathFinding; private set => _pathFinding = value; }
        public IMovementStrategy MovementStrategy { get => _movementStrategy; set => _movementStrategy = value; }

        public bool IsPlayerDetected => DetectPlayer() != null;


        private void Awake() {
            PathFinding = GetComponent<PathFinding>();
            Enemy = GetComponent<Enemy>();
            _target = GameManager.Instance.Player;
        }


        public Vector2 GetMovement() {
            // Define the movement strategy.
            if (MovementStrategy != null) {
                return MovementStrategy.GetMovement();
            }

            return Vector2.zero;
        }

        public bool GetFire() {
            if (IsPlayerDetected && Vector2.Distance(Enemy.transform.position, _target.transform.position) <= AttackRange) {
                // Get the direction of the attack.
                Vector2 direction = (_target.transform.position - Enemy.transform.position).normalized;

                // Check if the attack is horizontal or vertical.
                Vector2 facingDirection;
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                    facingDirection = new Vector2(Mathf.Sign(direction.x), 0);
                } else {
                    facingDirection = new Vector2(0, Mathf.Sign(direction.y));
                }

                // Set the facing direction of the enemy to the direction of the attack.
                Enemy.Character.StateMachine.CurrentFacingDirection = Enemy.Character.StateMachine.ConvertVectorToFacingDirection(facingDirection);

                
                // Get the player's velocity.
                Vector2 playerVelocity = _target.GetComponentInParent<Player>().GetComponent<Rigidbody2D>().velocity;

                // Attack the player when the player is not moving and is aligned with the enemy.
                if (playerVelocity == Vector2.zero && (Mathf.Abs(direction.x) <= _alignmentDirectionThreshold || Mathf.Abs(direction.y) <= _alignmentDirectionThreshold)) {
                    return true;
                }

                // Predict the player's position.
                float predictionTime = Vector2.Distance(Enemy.transform.position, _target.transform.position) / _attackSpeed;
                Vector2 predictedPosition = (Vector2)_target.transform.position + playerVelocity * predictionTime;

                // Attack the player when reaching the predicted position and the player is aligned with the enemy.
                if (Mathf.Abs(predictedPosition.x - Mathf.Round(predictedPosition.x)) <= _alignmentDirectionThreshold || Mathf.Abs(predictedPosition.y - Mathf.Round(predictedPosition.y)) <= _alignmentDirectionThreshold) {
                    return true;
                }
            }

            // Return false to not attack the player.
            return false;
        }


        // Detect the player in the detection range.
        private Transform DetectPlayer() {
            // Check if the player is in detection range.
            Collider2D hit = Physics2D.OverlapCircle(transform.position, DetectionRange, _playerLayer);

            // Check if the player is in line of sight.
            if (hit != null) {
                return hit.transform;
            }
            return null;
        }

        // Debugging Gizmos.
        private void OnDrawGizmos() {
            if (_showGizmos) {
                Gizmos.color = IsPlayerDetected ? _gizmoDetectedColor : _gizmoDetectionColor;
                Gizmos.DrawWireSphere(transform.position, DetectionRange);

                Gizmos.color = _gizmoAttackColor;
                Gizmos.DrawWireSphere(transform.position, AttackRange);
            }
        }
    }
}