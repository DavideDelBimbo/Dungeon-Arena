using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour {
    [Header("Spawner Settings")]
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private float _spawnRadius = 5f;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private Tilemap _groundTilemap;

    private List<Vector2> _validSpawnPositions = new();
    private readonly List<Enemy> _spawnedEnemies = new();


    private void Start() {
        // Gather all the valid spawn positions within the spawnable area.
        _validSpawnPositions = GatherValidSpawnPositions();

        // Invoke the SpawnEnemy method at the spawn rate.
        InvokeRepeating(nameof(SpawnEnemy), 0f, _spawnRate);
    }

    private List<Vector2> GatherValidSpawnPositions() {
        List<Vector2> validPositions = new();
        BoundsInt bounds = _groundTilemap.cellBounds;
        
        for (int x = bounds.xMin; x <= bounds.xMax; x++) {
            for (int y = bounds.yMin; y <= bounds.yMax; y++) {
                // Get the position of the current cell.
                Vector3Int cellPosition = new(x, y, (int) transform.position.z);
                Vector2 worldCellPosition = _groundTilemap.CellToWorld(cellPosition);

                // Check if the tile is within the spawnable radius.
                if (Vector2.Distance(worldCellPosition, transform.position) <= _spawnRadius && _groundTilemap.HasTile(cellPosition)) {
                    // Add the center of the cell to the valid spawn positions list.
                    validPositions.Add(worldCellPosition + ((Vector2)_groundTilemap.cellSize / 2));
                }
            }
        }

        return validPositions;
    }

    private void SpawnEnemy() {
        if (_spawnedEnemies.Count < _maxEnemies) {
            // Randomly select an enemy prefab.
            Enemy enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
    
            // Randomly select a non occupied spawn position.
            Vector2? spawnPosition = GetSpawnPosition();
            if (spawnPosition != null) {
                // Instantiate the enemy at the spawn position.
                Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPosition.Value, Quaternion.identity);
                StartCoroutine(spawnedEnemy.Character.Flash(Color.yellow, 0.1f, 3));

                // Add the spawned enemy to the spawned enemies list.
                _spawnedEnemies.Add(spawnedEnemy);
                spawnedEnemy.OnDeath += HandleEnemyDeath;
            }
        }
    }

    private Vector2? GetSpawnPosition() {
        // Randomize the valid spawn positions list.
        List<Vector2> randomValidSpawnPositions = _validSpawnPositions.OrderBy(x => Random.value).ToList();

        // Iterate over the valid spawn positions.
        foreach (Vector2 position in randomValidSpawnPositions) {
            // Check if some enemy is inside the cell.
            bool isFree = true;
            foreach (Enemy enemy in _spawnedEnemies) {
                // Get the cell position of the enemy.
                Vector3Int enemyCellPosition = _groundTilemap.WorldToCell(enemy.transform.position);

                // Check if the enemy is inside the cell.
                if (Vector2.Distance(position, enemy.transform.position) <= _groundTilemap.cellSize.x / 2) {
                    isFree = false;
                    break;
                }
            }

            // Return the position if it is not occupied.
            if (isFree) {
                return position;
            }
        }
        return null;
    }

    private void HandleEnemyDeath(IAgent enemy) {
        // Remove the enemy from the spawned enemies list.
        _spawnedEnemies.Remove(enemy as Enemy);
        ((Enemy) enemy).OnDeath -= HandleEnemyDeath;
    }


    // Debugging Gizmos.
    private void OnDrawGizmos() {
        // Draw a wire sphere to visualize the spawn radius.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}