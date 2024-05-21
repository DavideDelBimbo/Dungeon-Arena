using System;
using Unity.VisualScripting;
using UnityEngine;

public enum State { Idle, Walk, Attack }
public enum FacingDirection { Up, Down, Left, Right }    

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(IdleState))]
[RequireComponent(typeof(WalkState))]
[RequireComponent(typeof(AttackState))]
public class Character : MonoBehaviour {
    [Header("States")]
    [SerializeField] private State _initialState = State.Idle;
    [SerializeField] private FacingDirection _initialFacingDirection = FacingDirection.Down;

    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public AttackState AttackState { get; private set; }

    public State CurrentState { get; private set; }
    public FacingDirection CurrentFacingDirection { get; set; }

    public Movement Movement { get; private set; }

    private void Awake() {
        IdleState = GetComponent<IdleState>();
        WalkState = GetComponent<WalkState>();
        AttackState = GetComponent<AttackState>();

        CurrentFacingDirection = _initialFacingDirection;

        Movement = GetComponent<Movement>();
    }

    private void Start() {
        // Transition to the initial state.
        TransitionToState(_initialState);
    }

    private void Update() {
        // Update the current state.
        GetCharacterState(CurrentState).OnUpdate();
    }

    public void TransitionToState(State nextState) {
        GetCharacterState(CurrentState)?.OnExit();
        CurrentState = nextState;
        GetCharacterState(CurrentState).OnEnter();
    }

    private CharacterState GetCharacterState(State state) {
        return state switch {
            State.Idle => IdleState,
            State.Walk => WalkState,
            State.Attack => AttackState,
            _ => throw new ArgumentOutOfRangeException("Invalid state")
        };
    }

    public FacingDirection GetFacingDirection(Vector2 direction) {
        return direction switch {
            var dir when dir == Vector2.up => FacingDirection.Up,
            var dir when dir == Vector2.down => FacingDirection.Down,
            var dir when dir == Vector2.left => FacingDirection.Left,
            var dir when dir == Vector2.right => FacingDirection.Right,
            _ => CurrentFacingDirection
        };
    }
}
