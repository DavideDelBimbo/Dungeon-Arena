using UnityEngine;

public class Node {
    public Vector2 Position { get; }
    public bool IsWalkable { get; }
    public Node Parent { get; set; }
    public float GCost { get; set; }
    public float HCost { get; set; }
    public float FCost => GCost + HCost;


    public Node(Vector2 position, bool isWalkable) {
        Position = position;
        IsWalkable = isWalkable;
    }
}
