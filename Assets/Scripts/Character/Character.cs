using UnityEngine;

[RequireComponent(typeof(IdleState))]
[RequireComponent(typeof(WalkState))]
[RequireComponent(typeof(AttackState))]
public class Character : MonoBehaviour {
    public enum State { Idle, Walk, Attack }
    public enum FacingDirection { Up, Down, Left, Right }   


    [Header("Character Settings")]
    [SerializeField] private Sprite _previewSprite;

    [Header("States")]
    [SerializeField] private State _initialState = State.Idle;
    [SerializeField] private FacingDirection _initialFacingDirection = FacingDirection.Down;


    public Sprite PreviewSprite => _previewSprite;

    public Movement Movement { get; set; }
    public IInputHandler InputHandler { get; set; }

    public CharacterStateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public AttackState AttackState { get; private set; }


    private void Awake() {
        Movement ??= GetComponent<Movement>();
        InputHandler ??= GetComponent<IInputHandler>();

        StateMachine = new CharacterStateMachine(this);
        IdleState = GetComponent<IdleState>();
        WalkState = GetComponent<WalkState>();
        AttackState = GetComponent<AttackState>();
    }

    private void Start() {
        StateMachine.Initialize(_initialState, _initialFacingDirection);
    }

    private void Update() {
        StateMachine.Update();
    }
}
