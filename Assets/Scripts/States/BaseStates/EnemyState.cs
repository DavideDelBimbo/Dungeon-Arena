using UnityEngine;
using DungeonArena.CharacterControllers;
using DungeonArena.InputHandlers;
using DungeonArena.Managers;

namespace DungeonArena.States {

    [RequireComponent(typeof(Enemy))]
    public abstract class EnemyState : BaseState<Enemy> {
        [Header("Enemy State Settings")]
        [SerializeField] protected float _tolerance = 0.1f; // Tolerance value for the pathfinding.
        [SerializeField] protected float _maxDistanceFromPath = 1.5f; // Maximum distance from the path to recalculate the path.
        [SerializeField] protected float _recalculatePathDistanceThreshold = 2.0f; // Distance threshold to recalculate the path.
        [SerializeField] protected int _maxStepsBeforeRecalculate = 100; // Number of pathfinding steps before recalculating the path.


        protected AIInputHandler InputHandler => (AIInputHandler) _context.InputHandler;


        public override void OnEnter() {
            enabled = true;
        }

        public override void OnUpdate() { }

        public override void OnExit() {
            enabled = false;
        }
    }
}