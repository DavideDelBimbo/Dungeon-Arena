using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementStrategy : IMovementStrategy {
    private readonly Grid _grid;
    private readonly Enemy _enemy;
    private readonly EnemySpawner _enemySpawn;
    private readonly LayerMask _obstacleLayers;
    private readonly float _tolerance;

    private AStarPathfinding _pathfinding;
    private Queue<Node> _currentPath = new();
    private Node _currentNode;
    private float _maxDistanceFromPath = 1.5f;


    public PatrolMovementStrategy(Grid grid, Enemy enemy, EnemySpawner enemySpawn, LayerMask obstacleLayers, float tolerance = 0.1f) {
        _grid = grid;
        _enemy = enemy;
        _enemySpawn = enemySpawn;
        _obstacleLayers = obstacleLayers;
        _tolerance = tolerance;

        // Initialize the pathfinding algorithm.
        _pathfinding = new AStarPathfinding(grid);

        // Get the start node of the enemy.
        _currentNode = _grid.GetNode(_enemy.transform.position);
    }

    public Vector2 GetMovement() {
        // Move the enemy to the next node in the path if it exists.
        if (_currentPath.Count > 0) {
            return MoveToNextNode();
        }

        // Check if the enemy is close to the current node.
        if (Vector2.Distance(_enemy.transform.position, _grid.CellToCenterWorld(_currentNode.Position)) > _tolerance) {
            // Align the enemy with the current node.
            return AlignToCurrentNode();
        } else {
            // Calculate a new path.
            CalculateNewPath();
        }

        return Vector2.zero;
    }

    // Align the enemy with the current node.
    private Vector2 AlignToCurrentNode() {
        Vector2 enemyPosition = _enemy.transform.position;
        Vector2 direction = (_grid.CellToCenterWorld(_currentNode.Position) - enemyPosition).normalized;

        // Avoid diagonal movement.
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            direction.x = Mathf.Sign(direction.x);
            direction.y = 0;
        } else {
            direction.x = 0;
            direction.y = Mathf.Sign(direction.y);
        }

        return direction;
    }

    // Calculate a new path to the target.
    private void CalculateNewPath() {
        // Check if enemy is inside the enemy spawn.
        if (Vector2.Distance(_enemy.transform.position, _enemySpawn.transform.position) > _enemySpawn.SpawnRadius) {
            // Calculate the path to the enemy spawn.
            CalculatePathToTarget(_enemySpawn.transform.position);
        } else {
            // Define a random waypoint inside the spawn radius.
            Vector2 spawnPosition = _enemySpawn.transform.position;
            Vector2 randomWaypoint = spawnPosition + Random.insideUnitCircle * _enemySpawn.SpawnRadius;

            // Calculate the path to the random waypoint.
            CalculatePathToTarget(randomWaypoint);
        }
    }

    // Calculate the path to the enemy spawn.
    private void CalculatePathToTarget(Vector2 targetPosition) {
        // Get the target node.
        Node targetNode = _grid.GetNode(targetPosition);

        // Calculate the path to the target node.
        _currentPath = new Queue<Node>(_pathfinding.FindPath(_currentNode.Position, targetNode.Position));
    }

    // Move the enemy to the next node in the path.
    private Vector2 MoveToNextNode() {
        // Get the next node in the path.
        Node nextNode = _currentPath.Peek();

        // Calculate the distance from the enemy to the next node.
        float distanceFromNextNode = Vector2.Distance(_enemy.transform.position, _grid.CellToCenterWorld(nextNode.Position));

        // Recalculate the path if the enemy is blocked by an obstacle or if is too distant from the next node.
        Collider2D collider = Physics2D.OverlapCircle(_enemy.transform.position, 0.2f, _obstacleLayers);
        if (collider != null || distanceFromNextNode > _maxDistanceFromPath) {
            _currentPath.Clear();
            _currentNode = _grid.GetNode(_enemy.transform.position);
            return Vector2.zero;
        }

        // Calculate the direction to the next node.
        Vector2 direction = (nextNode.Position - _currentNode.Position).normalized;

        // Check if the enemy is close to the next node.
        if (distanceFromNextNode < _tolerance) {
            // Update the current node to the reached node.
            _currentNode = _currentPath.Dequeue();
        }

        return direction;
    }
}
