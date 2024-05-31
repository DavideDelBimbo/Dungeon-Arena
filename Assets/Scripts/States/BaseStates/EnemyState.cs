using UnityEngine;
using DungeonArena.CharacterControllers;
using DungeonArena.InputHandlers;

namespace DungeonArena.States {

    [RequireComponent(typeof(Enemy))]
    public abstract class EnemyState : BaseState<Enemy> {
        [Header("Enemy State Settings")]
        [SerializeField] protected float _tolerance = 0.1f;
        [SerializeField] protected float _maxDistanceFromPath = 1.5f;


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