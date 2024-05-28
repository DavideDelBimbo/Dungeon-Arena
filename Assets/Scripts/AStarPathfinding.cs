using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathfinding {
    private Grid _grid;

    public AStarPathfinding(Grid grid) {
        _grid = grid;
    }

    public List<Nodo> FindPath(Vector2 startPos, Vector2 targetPos) {
        Nodo startNode = _grid.GetNode(startPos);
        Nodo targetNode = _grid.GetNode(targetPos);
        if (startNode == null || targetNode == null) return null;

        List<Nodo> openList = new() { startNode };
        HashSet<Nodo> closedList = new();

        while (openList.Count > 0) {
            Nodo currentNode = openList.OrderBy(node => node.FCost).ThenBy(node => node.HCost).First();
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode) {
                return RetracePath(startNode, targetNode);
            }

            foreach (Nodo neighbor in _grid.GetNeighbors(currentNode)) {
                if (!neighbor.IsWalkable || closedList.Contains(neighbor)) continue;

                float newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.GCost || !openList.Contains(neighbor)) {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = GetDistance(neighbor, targetNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor)) {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private float GetDistance(Nodo nodeA, Nodo nodeB) {
        float dstX = Mathf.Abs(nodeA.Position.x - nodeB.Position.x);
        float dstY = Mathf.Abs(nodeA.Position.y - nodeB.Position.y);
        return dstX + dstY;
    }

    private List<Nodo> RetracePath(Nodo startNode, Nodo endNode) {
        List<Nodo> path = new();
        Nodo currentNode = endNode;
        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }
}
