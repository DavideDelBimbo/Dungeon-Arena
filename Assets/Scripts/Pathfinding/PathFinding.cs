using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DungeonArena.Managers;
using System;

namespace DungeonArena.Pathfinding {
    public class PathFinding : MonoBehaviour {
        [Header("Pathfinding Settings")]
        [SerializeField] private int _maxNodesProcessed = 1000;

        [Header("Gizmos Settings")]
        [SerializeField] private bool _showPath = true;

        private List<Node> _path = new();
        private List<Node> _dynamicObstacles = new();


        // Find the path from the start position to the target position.
        public List<Node> FindPath(Vector3 startPosition, Vector3 targetPosition, List<Node> dynamicObstacles = null) {
            Node startNode = GridManager.Instance.GetNodeFromWorldPoint(startPosition);
            Node targetNode = GridManager.Instance.GetNodeFromWorldPoint(targetPosition);
            if (startNode == null || targetNode == null) return null;

            // Initialize the open and closed lists.
            List<Node> openList = new() { startNode }; // List of nodes to be evaluated.
            HashSet<Node> closedList = new(); // List of nodes already evaluated.

            _dynamicObstacles = dynamicObstacles;
            int nodesProcessed = 0;

            // Loop until the open list is empty.
            while (openList.Count > 0) {
                if (nodesProcessed > _maxNodesProcessed) {
                    Debug.LogWarning("Pathfinding aborted: too many nodes processed");
                    break;
                }

                // Get the node with the lowest F cost (or H cost if there is a tie).
                Node currentNode = openList.OrderBy(node => node.FCost).ThenBy(node => node.HCost).First();
                openList.Remove(currentNode); // Remove the current node from the evaluation list.
                closedList.Add(currentNode); // Add the current node to the processed list.

                // Return the path if target node is reached.
                if (currentNode == targetNode) {
                    return RetracePath(startNode, targetNode);
                }

                foreach (Node neighbor in GridManager.Instance.GetNeighbors(currentNode).Where(n => n.IsWalkable && (!_dynamicObstacles?.Contains(n) ?? true) && !closedList.Contains(n))) {
                    // Get the movement cost to the neighbor.
                    float costToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);

                    // Check if the neighbor is not in the open list or has a lower G cost.
                    if (!openList.Contains(neighbor) || costToNeighbor < neighbor.GCost) {
                        neighbor.GCost = costToNeighbor;
                        neighbor.HCost = GetDistance(neighbor, targetNode);
                        neighbor.Parent = currentNode;

                        if (!openList.Contains(neighbor)) {
                            openList.Add(neighbor);
                        }
                    }
                }
                
                // Update the number of nodes processed.
                nodesProcessed++;
            }
            
            // Return an empty list if no path is found.
            _path = new List<Node>();
            return new List<Node>();
        }


        // Retrace the path from the end node to the start node.
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
            _path = path;
            return path;
        }

        // Calculate the distance between two nodes.
        private float GetDistance(Node nodeA, Node nodeB) {
            // Calculate the Manhattan distance between two nodes.
            float dstX = Mathf.Abs(nodeA.GridPosition.x - nodeB.GridPosition.x);
            float dstY = Mathf.Abs(nodeA.GridPosition.y - nodeB.GridPosition.y);
            return dstX + dstY;
        }


        // Debugging Gizmos.
        private void OnDrawGizmos() {
            if (_path.Count > 0 && _showPath) {
                Gizmos.color = Color.green;
                _path.ForEach(node => { Gizmos.DrawCube(node.WorldCenterPosition, 0.5f * GridManager.Instance.CellSize * Vector2.one); });
            
                Gizmos.color = Color.red;
                _dynamicObstacles?.ForEach(node => { Gizmos.DrawCube(node.WorldCenterPosition, 0.5f * GridManager.Instance.CellSize * Vector2.one); });
            }
        }
    }
}