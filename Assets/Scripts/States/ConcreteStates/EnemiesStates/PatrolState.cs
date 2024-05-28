using UnityEngine;
using UnityEngine.Tilemaps;

public class PatrolState : EnemyState {
    [Header("Patrol Settings")]
    [SerializeField] private Tilemap _walkableTilemap;
    [SerializeField] private LayerMask _obstacleLayers;
    

    private PatrolMovementStrategy _movementStrategy;


    public Tilemap WalkableTilemap { get => _walkableTilemap; set => _walkableTilemap = value; }


    public override void OnEnter() {
        base.OnEnter();

        // Set the movement strategy.
        Grid grid = new(_walkableTilemap);
        _movementStrategy = new PatrolMovementStrategy(grid, _context, _context.Spawner, _obstacleLayers);
        InputHandler.SetMovementStrategy(_movementStrategy);
    }

    public override void OnUpdate() {
        base.OnUpdate();

        
        
        // Transition to Chase state if player is detected.
        /*if (InputHandler.IsPlayerDetected) {
            StopCoroutine(ChangeDirection());
            _context.StateMachine.TransitionToState(_context.ChaseState);
        }*/
    }

    /*private IEnumerator ChangeDirection() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(_randomRangeTime.x, _randomRangeTime.y));

            // Change direction after a random time.
            _movementStrategy.UpdateDirection(_context.transform.position, _waypoint.position);
        }
    }*/


    /*private void OnDrawGizmos() {
        if (_waypoint != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_waypoint.position, _patrolRadius);
        }
    }*/

    /*private void OnTriggerStay2D(Collider2D other) {
        if (!enabled) return;

        if (other.TryGetComponent<Node>(out var node) && node != _movementStrategy.CurrentNode) {
            _movementStrategy.CurrentNode = node;
        }
    }*/
}
