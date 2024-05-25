using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PatrolState : EnemyState {
    [SerializeField] Transform _waypoint;
    [SerializeField, Range(0, 10)] float _patrolRadius = 5.0f;
    [SerializeField] Vector2 _randomRangeTime = new(1f, 3f);

    private PatrolMovementStrategy _movementStrategy;
    
    /*public override void OnEnter() {
        base.OnEnter();

        // Set the target.
        InputHandler.SetTarget(_waypoint);

        // Set the movement strategy.
        _movementStrategy = new PatrolMovementStrategy(_patrolRadius);
        InputHandler.SetMovementStrategy(_movementStrategy);

        // Start changing direction strategy.
        StartCoroutine(ChangeDirection());
    }

    public override void OnUpdate() {
        base.OnUpdate();
        
        // Transition to Chase state if player is detected.
        if (InputHandler.IsPlayerDetected) {
            StopCoroutine(ChangeDirection());
            _context.StateMachine.TransitionToState(_context.ChaseState);
        }
    }

    private IEnumerator ChangeDirection() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(_randomRangeTime.x, _randomRangeTime.y));

            // Change direction after a random time.
            _movementStrategy.UpdateDirection(_context.transform.position, _waypoint.position);
        }
    }


    private void OnDrawGizmos() {
        if (_waypoint != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_waypoint.position, _patrolRadius);
        }
    }*/
}
