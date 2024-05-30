using System;
using System.Collections.Generic;
using UnityEngine;
using DungeonArena.Managers;

namespace DungeonArena.Pathfinding {
    public class GridManager : Singleton<GridManager> {
        [Header("Grid Settings")]
        [SerializeField] private Vector2 _mapSize;
        [SerializeField] private Vector2 _originPosition;
        [SerializeField, Range(0.1f, 1f)] private float _cellSize;
        [SerializeField] private LayerMask _obstaclesLayer;

        [Header("Gizmo Settings")]
        [SerializeField] private bool _showGrid = false;

        private Node[,] _grid;


        public Vector2Int GridSize => new(Mathf.RoundToInt(_mapSize.x / _cellSize), Mathf.RoundToInt(_mapSize.y / _cellSize));
        public Vector2 OriginPosition => _originPosition;
        public Vector2 Center => _originPosition + _mapSize / 2;
        public float CellSize => _cellSize;


        protected override void Awake() {
            base.Awake();
            InitializeGrid();
        }


        private void InitializeGrid() {
            _grid = new Node[GridSize.x, GridSize.y];

            for (int x = 0; x < GridSize.x; x++) {
                for (int y = 0; y < GridSize.y; y++) {
                    // Calculate the grid position of the node.
                    Vector2Int nodeGridPosition = new(x, y);

                    // Calculate the world position of the node.
                    Vector2 nodeWorldPosition = new Vector2(x, y) * _cellSize + _originPosition;

                    // Calculate the world center position of the node.
                    Vector2 nodeWorldCenterPosition = nodeWorldPosition + new Vector2(_cellSize / 2, _cellSize / 2);

                    // Check if the node is walkable.
                    Collider2D collider = Physics2D.OverlapCircle(nodeWorldCenterPosition, _cellSize / 2, _obstaclesLayer);
                    bool isWalkable = !(collider != null && collider.bounds.Contains(nodeWorldPosition));
                    
                    _grid[x, y] = new Node(nodeGridPosition, nodeWorldPosition, nodeWorldCenterPosition, isWalkable);
                }
            }
        }


        // Get node on the grid at the given position.
        public Node GetNode(Vector2Int position) {
            if (position.x >= 0 && position.x < GridSize.x && position.y >= 0 && position.y < GridSize.y) {
                return _grid[position.x, position.y];
            }

            return null;
        }

        // Get node on the grid from world point position.
        public Node GetNodeFromWorldPoint(Vector2 worldPoint) {
            int x = Mathf.FloorToInt((worldPoint.x - _originPosition.x) / _cellSize);
            int y = Mathf.FloorToInt((worldPoint.y - _originPosition.y) / _cellSize);
            return GetNode(new Vector2Int(x, y));
        }

        // Get the neighbors of the given node on the grid.
        public List<Node> GetNeighbors(Node node) {
            List<Node> neighbours = new();
            List<Vector2Int> availableDirections = new() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (Vector2Int direction in availableDirections) {
                Vector2Int neighbourPosition = node.GridPosition + direction;
                Node neighbour = GetNode(neighbourPosition);

                if (neighbour != null && neighbour.IsWalkable) {
                    neighbours.Add(neighbour);
                }
            }

            return neighbours;
        }


        // Debugging Gizmos.
        private void OnDrawGizmos() {
            if (_showGrid) {
                // Draw the grid.
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(Center, _mapSize);

                if (_grid != null) {
                    foreach (Node node in _grid) {
                        Gizmos.color = node.IsWalkable ? Color.white : Color.red;
                        Gizmos.DrawWireCube(node.WorldCenterPosition, Vector2.one * _cellSize);
                    }
                }
            }
        }
    }
}