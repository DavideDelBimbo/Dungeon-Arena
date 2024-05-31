using DungeonArena.CharacterControllers;
using UnityEngine;

namespace DungeonArena.Strategies.MovementStrategies {
    public class WaitMovementStrategy : BaseMovementStrategy {
        public WaitMovementStrategy(Enemy enemy, float tolerance = 0.1f, float maxDistanceFromPath = 1.5f) : base(enemy, tolerance, maxDistanceFromPath) {}

        public override Vector2 GetMovement() {
            return Vector2.zero;
        }
    }
}