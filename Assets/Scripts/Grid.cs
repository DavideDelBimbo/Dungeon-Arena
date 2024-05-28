using System.Collections.Generic;
using UnityEngine;

public class Grid {
    private int _width;
    private int _height;
    private float _nodeSize;
    private Nodo[,] _nodes;

    public Grid(int width, int height, float nodeSize) {
        _width = width;
        _height = height;
        _nodeSize = nodeSize;
        _nodes = new Nodo[width, height];
        InitializeGrid();
    }

    private void InitializeGrid() {
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                Vector2 worldPosition = new(x * _nodeSize, y * _nodeSize);
                bool isWalkable = !Physics2D.OverlapCircle(worldPosition, _nodeSize / 2, LayerMask.GetMask("Obstacles") | LayerMask.GetMask("Forbidden"));
                _nodes[x, y] = new Nodo(worldPosition, isWalkable);
            }
        }
    }

    public Nodo GetNode(Vector2 position) {
        int x = Mathf.FloorToInt(position.x / _nodeSize);
        int y = Mathf.FloorToInt(position.y / _nodeSize);
        if (x >= 0 && x < _width && y >= 0 && y < _height) {
            return _nodes[x, y];
        }
        return null;
    }

    public IEnumerable<Nodo> GetNeighbors(Nodo node) {
        List<Nodo> neighbors = new();
        for (int dx = -1; dx <= 1; dx++) {
            for (int dy = -1; dy <= 1; dy++) {
                if (dx == 0 && dy == 0) continue;

                int checkX = (int)(node.Position.x + dx);
                int checkY = (int)(node.Position.y + dy);
                if (checkX >= 0 && checkX < _width && checkY >= 0 && checkY < _height) {
                    neighbors.Add(_nodes[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }
}