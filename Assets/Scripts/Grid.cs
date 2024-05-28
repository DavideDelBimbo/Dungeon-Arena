using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;


public class Grid {
    private readonly Tilemap _tilemap;
    private readonly List<Node> _nodes;


    public Grid(Tilemap tilemap) {
        _tilemap = tilemap;
        _nodes = new List<Node>();

        // Initialize the grid.
        InitializeGrid();
    }


    // Initialize the grid with nodes.
    private void InitializeGrid() {
        foreach (Vector3Int position in _tilemap.cellBounds.allPositionsWithin) {
            // Check if the node is walkable.
            bool isWalkable = _tilemap.HasTile(position);

            // Calculate the position of the node.
            Vector2 nodePosition = new(position.x, position.y);

            // Create the node.
            Node node = new(nodePosition, isWalkable);
            _nodes.Add(node);
        }
    }


    // Get the node on the grid at the given position.
    public Node GetNode(Vector2 position) {
        // Calculate the position on the grid.
        int x = Mathf.FloorToInt(position.x / _tilemap.cellSize.x);
        int y = Mathf.FloorToInt(position.y / _tilemap.cellSize.y);

        // Get the node at the given position.
        return _nodes.Find(node => node.Position == new Vector2(x, y));
    }

    // Get the neighbors of the given node.
    public List<Node> GetNeighbors(Node node) {
        Vector2[] availableDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right }; // List of admissible directions.
        List<Node> neighbors = new(); // List of neighbors.

        foreach (Vector2 direction in availableDirections) {
            // Calculate the neighbor position from the current node at the given direction.
            Vector2 neighborPosition = node.Position + direction;

            // Get the neighbor node based on the direction.
            Node neighbor = GetNode(neighborPosition);

            // Add the neighbor to the list if it is walkable.
            if (neighbor != null && neighbor.IsWalkable) {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    // Get center world position from cell position on the grid.
    public Vector2 CellToCenterWorld(Vector2 cellPosition) {
        Vector3Int cell = new((int) cellPosition.x, (int) cellPosition.y, 0);
        return _tilemap.GetCellCenterWorld(cell);
    }
}
