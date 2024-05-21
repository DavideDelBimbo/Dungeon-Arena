using System;
using Unity.VisualScripting;
using UnityEngine;

public enum CharacterState { Idle, Walk, Attack }
public enum FacingDirection { Up, Down, Left, Right }    

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(IInputHandler))]
[RequireComponent(typeof(IdleState))]
[RequireComponent(typeof(WalkState))]
[RequireComponent(typeof(AttackState))]
public class Character : MonoBehaviour {
    [Header("States")]
    [SerializeField] private CharacterState _initialState = CharacterState.Idle;
    [SerializeField] private FacingDirection _initialFacingDirection = FacingDirection.Down;

    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public AttackState AttackState { get; private set; }

    public CharacterBaseState CurrentState { get; private set; }
    public FacingDirection CurrentFacingDirection { get; set; }

    public Movement Movement { get; private set; }
    public IInputHandler InputHandler { get; set; }

    private void Awake() {
        IdleState = GetComponent<IdleState>();
        WalkState = GetComponent<WalkState>();
        AttackState = GetComponent<AttackState>();

        CurrentFacingDirection = _initialFacingDirection;

        Movement = GetComponent<Movement>();
        InputHandler = GetComponent<IInputHandler>();
    }

    private void Start() {
        // Transition to the initial state.
        TransitionToState(_initialState);
    }

    private void Update() {
        // Update the current state.
        CurrentState.OnUpdate();
    }

    public void TransitionToState(CharacterState nextState) {
        CurrentState?.OnExit();
        CurrentState = GetCharacterState(nextState);
        CurrentState.OnEnter();
    }

    private CharacterBaseState GetCharacterState(CharacterState state) {
        return state switch {
            CharacterState.Idle => IdleState,
            CharacterState.Walk => WalkState,
            CharacterState.Attack => AttackState,
            _ => throw new ArgumentOutOfRangeException("Invalid character state")
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
