using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathfinding {
    private readonly Grid _grid;

    public AStarPathfinding(Grid grid) {
        _grid = grid;
    }

    public List<Node> FindPath(Vector2 startPosition, Vector2 targetPosition) {
        Node startNode = _grid.GetNode(startPosition);
        Node targetNode = _grid.GetNode(targetPosition);
        if (startNode == null || targetNode == null) return null;

        // Initialize the open and closed lists.
        List<Node> openList = new() { startNode }; // List of nodes to be evaluated.
        HashSet<Node> closedList = new(); // List of nodes already evaluated.

        // Loop until the open list is empty.
        while (openList.Count > 0) {
            // Get the node with the lowest F cost (or H cost if there is a tie).
            Node currentNode = openList.OrderBy(node => node.FCost).ThenBy(node => node.HCost).First();
            openList.Remove(currentNode); // Remove the current node from the evaluation list.
            closedList.Add(currentNode); // Add the current node to the processed list.

            // Return the path if target node is reached.
            if (currentNode == targetNode) {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbor in _grid.GetNeighbors(currentNode).Where(n => n.IsWalkable && !closedList.Contains(n))) {
                // Get the movement cost to the neighbor.
                float costToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);

                // Check if the neighbor is not in the open list or has a lower G cost.
                if (!openList.Contains(neighbor) || costToNeighbor < neighbor.GCost) {
                    neighbor.GCost = costToNeighbor;
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor)) {
                        neighbor.HCost = GetDistance(neighbor, targetNode);
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return new List<Node>();
    }

    private float GetDistance(Node nodeA, Node nodeB) {
        // Calculate the Manhattan distance between two nodes.
        float dstX = Mathf.Abs(nodeA.Position.x - nodeB.Position.x);
        float dstY = Mathf.Abs(nodeA.Position.y - nodeB.Position.y);
        return dstX + dstY;
    }

    private List<Node> RetracePath(Node startNode, Node endNode) {
        List<Node> path = new(); // List of nodes in the path.

        // Retrace the path from the end node to the start node.
        Node currentNode = endNode;
        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        // Reverse the path to get the correct order.
        path.Reverse();
        return path;
    }
}
