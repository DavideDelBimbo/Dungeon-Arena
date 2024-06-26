using UnityEngine;
using DungeonArena.Interfaces;
using DungeonArena.CharacterControllers;

namespace DungeonArena.States {

    [RequireComponent(typeof(Character))]
    public abstract class CharacterState : BaseState<Character> {
        [Header("State Settings")]
        [SerializeField] private StateAnimation _stateAnimation;


        protected StateAnimation StateAnimation {
            get => _stateAnimation;
            private set => _stateAnimation = value;
        }

        protected IInputHandler InputHandler => _context.InputHandler;


        public override void OnEnter() {
            enabled = true;

            // Update the facing direction based on the current movement direction.
            _context.StateMachine.CurrentFacingDirection = _context.StateMachine.ConvertVectorToFacingDirection(_context.Movement.CurrentDirection);

            // Play the animation based on the current facing direction.
            StateAnimation.Play(_context.StateMachine.CurrentFacingDirection);
        }

        public override void OnUpdate() { }

        public override void OnExit() {
            enabled = false;

            // Stop the animation.
            StateAnimation.Stop();
        }
    }
}