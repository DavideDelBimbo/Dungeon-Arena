using UnityEngine;

public class Nodo {
    public Vector2 Position { get; }
    public bool IsWalkable { get; }
    public Nodo Parent { get; set; }
    public float GCost { get; set; }
    public float HCost { get; set; }
    public float FCost => GCost + HCost;

    public Nodo(Vector2 position, bool isWalkable) {
        Position = position;
        IsWalkable = isWalkable;
    }
}
