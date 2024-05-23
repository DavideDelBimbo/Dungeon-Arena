using UnityEngine;

public interface IMovementStrategy {
    Vector2 GetMovement();
    //Vector2 GetMovement(Enemy enemy, Transform target);
}