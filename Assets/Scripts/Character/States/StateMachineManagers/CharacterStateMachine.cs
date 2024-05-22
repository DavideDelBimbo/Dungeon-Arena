using System.ComponentModel;
using UnityEngine;
using static Character;

public class CharacterStateMachine : StateMachine<Character, CharacterState> {
    public FacingDirection CurrentFacingDirection { get; set; }


    public CharacterStateMachine(Character context) : base(context) { }


    public void Initialize(State initialState, FacingDirection initialFacingDirection) {
        CurrentFacingDirection = initialFacingDirection;

        // Transition to the initial state.
        CharacterState intialCharacterState = ConvertStateToCharacterState(initialState);
        TransitionToState(intialCharacterState);
    }


    // Convert the state value to the character state object.
    public CharacterState ConvertStateToCharacterState(State state) => state switch {
        State.Idle => _context.IdleState,
        State.Walk => _context.WalkState,
        State.Attack => _context.AttackState,
        _ => throw new InvalidEnumArgumentException($"Invalid character state value: {state}.")
    };

    // Convert the vector direction to the facing direction value.
    public FacingDirection ConvertVectorToFacingDirection(Vector2 direction) => direction switch {
        Vector2 dir when dir == Vector2.up => FacingDirection.Up,
        Vector2 dir when dir == Vector2.down => FacingDirection.Down,
        Vector2 dir when dir == Vector2.left => FacingDirection.Left,
        Vector2 dir when dir == Vector2.right => FacingDirection.Right,
        _ => CurrentFacingDirection
    };
}