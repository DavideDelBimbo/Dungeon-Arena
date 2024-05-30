using UnityEngine;

namespace DungeonArena.Pathfinding {
    public class Node {
        public Vector2Int GridPosition { get; }
        public Vector2 WorldPosition { get; }
        public Vector2 WorldCenterPosition { get; }
        public bool IsWalkable { get; }
        public Node Parent { get; set; }
        public float GCost { get; set; }
        public float HCost { get; set; }
        public float FCost => GCost + HCost;


        public Node(Vector2Int gridPosition, Vector2 worldPosition, Vector2 worldPositionCenter, bool isWalkable) {
            GridPosition = gridPosition;
            WorldPosition = worldPosition;
            WorldCenterPosition = worldPositionCenter;
            IsWalkable = isWalkable;
        }
    }
}