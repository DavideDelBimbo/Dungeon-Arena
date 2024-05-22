using UnityEngine;

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
        // Play the animation based on the current facing direction.
        StateAnimation.Play(_context.StateMachine.CurrentFacingDirection);
    }

    public override void OnUpdate() { }

    public override void OnExit() {
        // Stop the animation.
        StateAnimation.Stop();
    }
}
