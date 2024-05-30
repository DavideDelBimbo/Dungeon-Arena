using UnityEngine;

namespace DungeonArena.Interfaces {
    public interface IInputHandler {
        Vector2 GetMovement();
        bool GetFire();
    }
}