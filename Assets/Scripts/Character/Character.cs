using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IdleState))]
[RequireComponent(typeof(WalkState))]
[RequireComponent(typeof(AttackState))]
[RequireComponent(typeof(DeadState))]
public class Character : MonoBehaviour {
    public enum State { Idle, Walk, Attack, Dead }
    public enum FacingDirection { Up, Down, Left, Right }   


    [Header("Character Settings")]
    [SerializeField] private Sprite _previewSprite;
    [SerializeField] private Color _deadFlashColor = Color.red;
    [SerializeField] private float _flashDuration = 0.1f;


    [Header("States")]
    [SerializeField] private State _initialState = State.Idle;
    [SerializeField] private FacingDirection _initialFacingDirection = FacingDirection.Down;


    public Color DeadFlashColor => _deadFlashColor;
    public float FlashDuration => _flashDuration;

    public Sprite PreviewSprite => _previewSprite;

    public IAgent Agent { get; set; }
    public Movement Movement { get; set; }
    public IInputHandler InputHandler { get; set; }

    public IWeapon Weapon { get; set; }

    public CharacterStateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public AttackState AttackState { get; private set; }
    public DeadState DeadState { get; private set; }


    private void Awake() {
        Agent ??= GetComponentInParent<IAgent>();
        Movement = (Movement != null) ? Movement : GetComponent<Movement>();
        InputHandler ??= GetComponent<IInputHandler>();
    
        Weapon ??= GetComponentInChildren<IWeapon>();

        StateMachine = new CharacterStateMachine(this);
        IdleState = GetComponent<IdleState>();
        WalkState = GetComponent<WalkState>();
        AttackState = GetComponent<AttackState>();
        DeadState = GetComponent<DeadState>();
    }

    private void Start() {
        StateMachine.Initialize(_initialState, _initialFacingDirection);
    }

    private void Update() {
        StateMachine.Update();
    }


    // Flash the character sprite.
    public IEnumerator Flash(Color color, float duration = 0.1f, int times = 1) {
        // Flash the character sprite.
        for (int i = 0; i < times; i++) {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                spriteRenderer.color = color;
            }
            yield return new WaitForSeconds(duration);

            // Reset the character sprite color.
            foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                spriteRenderer.color = Color.white;
            }
            yield return new WaitForSeconds(duration);
        }

        yield break;
    }
}
