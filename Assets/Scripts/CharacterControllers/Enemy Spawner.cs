using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using DungeonArena.Interfaces;
using DungeonArena.Managers;

namespace DungeonArena.CharacterControllers {
    public class EnemySpawner : MonoBehaviour {
        [Header("Spawner Settings")]
        [SerializeField] private List<Enemy> _enemyPrefabs;
        [SerializeField] private float _spawnRate = 1.0f;
        [SerializeField] private float _spawnRadius = 5.0f;
        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private float _cellSize = 1.0f;
        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private LayerMask _obstacleLayers;

        [Header("Gizmo Settings")]
        [SerializeField] private bool _drawGizmos = true;

        private List<Vector2> _validSpawnPositions = new();
        private readonly List<Enemy> _spawnedEnemies = new();


        public float SpawnRadius => _spawnRadius;
        public List<Vector2> ValidSpawnPositions => _validSpawnPositions;


        private void Start() {
            // Gather all the valid spawn positions within the spawnable area.
            _validSpawnPositions = GatherValidSpawnPositions();

            // Invoke the SpawnEnemy method at the spawn rate.
            InvokeRepeating(nameof(SpawnEnemy), 0f, _spawnRate);
        }


        // Gather all the valid spawn positions within the spawnable area.
        private List<Vector2> GatherValidSpawnPositions() {
            List<Vector2> validPositions = new(); // List to store the valid spawn positions.
            Vector2 spawnerOrigin = transform.position; // Get the position of the spawner. 
            
            for (float x = spawnerOrigin.x - _spawnRadius; x <= spawnerOrigin.x + _spawnRadius; x += _cellSize) {
                for (float y = spawnerOrigin.y - _spawnRadius; y <= spawnerOrigin.y + _spawnRadius; y += _cellSize) {
                    // Get the position of the current cell.
                    Vector2 cellPosition = new(x, y);

                    // Check if the cell is within the spawnable radius and is a ground tile and there is no obstacle in the cell.
                    if (Vector2.Distance(spawnerOrigin, cellPosition) <= _spawnRadius &&
                            _groundTilemap.HasTile(new Vector3Int((int)cellPosition.x, (int)cellPosition.y, 0)) &&
                            !Physics2D.OverlapCircle(cellPosition, _cellSize / 2, _obstacleLayers)) {
                        // Add the the cell to the valid spawn positions list.
                        validPositions.Add(cellPosition);
                    }
                }
            }

            return validPositions;
        }

        // Spawn an enemy at a random valid spawn position.
        private void SpawnEnemy() {
            if (_spawnedEnemies.Count < _maxEnemies) {
                // Randomly select an enemy prefab.
                Enemy enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
        
                // Randomly select a non occupied spawn position.
                Vector2? spawnPosition = GetSpawnPosition();
                if (spawnPosition != null) {
                    // Instantiate the enemy at the spawn position.
                    Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPosition.Value, Quaternion.identity);
                    spawnedEnemy.Spawner = this;
                    GameManager.Instance.Enemies.Add(spawnedEnemy);
                    StartCoroutine(spawnedEnemy.Character.Flash(spawnedEnemy.SpawnFlashMaterial, spawnedEnemy.SpawnFlashDuration, 3));

                    // Add the spawned enemy to the spawned enemies list.
                    _spawnedEnemies.Add(spawnedEnemy);
                    spawnedEnemy.OnDeath += HandleEnemyDeath;
                }
            }
        }

        // Get a random non occupied spawn position.
        private Vector2? GetSpawnPosition() {
            // Randomize the valid spawn positions list.
            List<Vector2> shuffledSpawnPositions = _validSpawnPositions.OrderBy(x => Random.value).ToList();

            // Iterate over the valid spawn positions.
            foreach (Vector2 position in shuffledSpawnPositions) {
                // Check if the position is free to spawn an enemy.
                if (IsPositionFree(position)) {
                    return position;
                }
            }

            return null;
        }

        // Check if the position is free to spawn an enemy.
        private bool IsPositionFree(Vector2 position) {
            // Check if the player is inside the cell.
            if (Vector2.Distance(position, GameManager.Instance.Player.transform.position) <= _cellSize / 2) {
                return false;
            }

            // Check if some enemy is inside the cell.
            foreach (Enemy enemy in _spawnedEnemies) {
                // Check if the enemy is inside the cell.
                if (Vector2.Distance(position, enemy.transform.position) <= _cellSize / 2) {
                    return false;
                }
            }

            return true;
        }

        // Handle the enemy death event.
        private void HandleEnemyDeath(IAgent enemy) {
            // Remove the enemy from the spawned enemies list.
            _spawnedEnemies.Remove(enemy as Enemy);
            GameManager.Instance.Enemies.Remove(enemy as Enemy);
            enemy.OnDeath -= HandleEnemyDeath;
        }


        // Debugging Gizmos.
        private void OnDrawGizmos() {
            if (!_drawGizmos) return;

            // Draw a wire sphere to visualize the spawn radius.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);

            // Draw a wire sphere for each valid spawn position.
            if (_validSpawnPositions != null) {
                Gizmos.color = Color.yellow.WithAlpha(0.2f);
                _validSpawnPositions.ForEach(validPosition => Gizmos.DrawWireSphere(validPosition, _cellSize / 2));
            }
        }
    }
}


