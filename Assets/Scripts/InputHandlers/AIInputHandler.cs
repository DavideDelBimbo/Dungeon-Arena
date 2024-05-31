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
        [SerializeField] private LayerMask _playerLayer;

        [Header("Gizmos Settings")]
        [SerializeField] private bool _showGizmos = true;
        [SerializeField] private Color _gizmoDetectionColor = Color.green;
        [SerializeField] private Color _gizmoDetectedColor = Color.red;
        [SerializeField] private Color _gizmoAttackColor = Color.blue;

        private Enemy _enemy;
        private PathFinding _pathFinding;
        private IMovementStrategy _movementStrategy;

        private Player Target => GameManager.Instance.Player;


        public float DetectionRange => _detectionRange;
        public float AttackRange => _attackRange;

        public Enemy Enemy { get => _enemy; private set => _enemy = value; }
        public PathFinding PathFinding { get => _pathFinding; private set => _pathFinding = value; }
        public IMovementStrategy MovementStrategy { get => _movementStrategy; set => _movementStrategy = value; }

        public bool IsPlayerDetected => DetectPlayer() != null;


        private void Awake() {
            PathFinding = GetComponent<PathFinding>();
            Enemy = GetComponent<Enemy>();
        }


        public Vector2 GetMovement() {
            // Define the movement strategy.
            if (MovementStrategy != null) {
                return MovementStrategy.GetMovement();
            }

            return Vector2.zero;
        }

        public bool GetFire() {
            if (IsPlayerDetected) {                
                // Predict the player's position and attack direction.
                float predictionTime = Vector2.Distance(Enemy.transform.position, Target.transform.position) / _attackSpeed;
                Vector2 predictedPosition = (Vector2) Target.transform.position + Target.Movement.Body.velocity * predictionTime;
                Vector2 predictedDirection = (predictedPosition - (Vector2) Enemy.transform.position).normalized;

                // Set the facing direction of the enemy to the predicted attack direction.
                if (Mathf.Abs(predictedDirection.x) > Mathf.Abs(predictedDirection.y)) {
                    // Horizontal direction.
                    Enemy.Character.StateMachine.CurrentFacingDirection = Enemy.Character.StateMachine.ConvertVectorToFacingDirection(new Vector2(Mathf.Sign(predictedDirection.x), 0));
                } else {
                    // Vertical direction.
                    Enemy.Character.StateMachine.CurrentFacingDirection = Enemy.Character.StateMachine.ConvertVectorToFacingDirection(new Vector2(0, Mathf.Sign(predictedDirection.y)));
                }

                // Check if the predicted position of the player is in range.
                float distanceToPredictedPosition = Vector2.Distance(Enemy.transform.position, predictedPosition);

                // Return true to attack the player.
                if (distanceToPredictedPosition <= AttackRange) {
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