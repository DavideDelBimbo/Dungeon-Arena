using UnityEngine;
using DungeonArena.Interfaces;

namespace DungeonArena.States {
    public abstract class BaseState<T> : MonoBehaviour, IState where T : MonoBehaviour {
        protected T _context;


        protected virtual void Awake() {
            _context = GetComponent<T>();
        }

        protected virtual void Start() {
            enabled = false;
        }


        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void OnUpdate();
    }
}