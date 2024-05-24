using UnityEngine;

public abstract class StateMachine<T, S> where T : MonoBehaviour where S : BaseState<T> {
    protected T _context;


    public S CurrentState { get; private set; }


    public StateMachine(T context) {
        _context = context;
    }


    public virtual void Update() {
        // Update the current state.
        CurrentState?.OnUpdate();
    }

    public virtual void TransitionToState(S newState) {
        // Exit the current state and enter the new state.
        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}

