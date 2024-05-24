using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(IInputHandler))]
[RequireComponent(typeof(WaitState))]
[RequireComponent(typeof(PatrolState))]
[RequireComponent(typeof(ChaseState))]
[RequireComponent(typeof(VulnerableState))]
public class Enemy : MonoBehaviour , IAgent, IDamageble {
    public enum State { Wait, Patrol, Chase, Vulnerable }


    [Header("Enemy Settings")]
    [SerializeField] private Player _player;
    [field: SerializeField] public int Health { get; set; } = 3;

    [Header("States")]
    [SerializeField] private State _initialState = State.Wait;

    public Character Character { get; private set; }
    public Movement Movement { get; private set; }
    public IInputHandler InputHandler { get; private set; }

    public Player Player { get => _player; private set => _player = value; }

    public EnemyStateMachine StateMachine { get; private set; }
    public WaitState WaitState { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public VulnerableState VulnerableState { get; private set; }

    private void Awake() {
        Character = GetComponentInChildren<Character>();
        Movement = GetComponent<Movement>();
        InputHandler = GetComponent<IInputHandler>();

        // Dependency injection.
        Character.Movement = Movement;
        Character.InputHandler = InputHandler;

        StateMachine = new EnemyStateMachine(this);
        WaitState = GetComponent<WaitState>();
        PatrolState = GetComponent<PatrolState>();
        ChaseState = GetComponent<ChaseState>();
        VulnerableState = GetComponent<VulnerableState>();
    }

    private void Start() {
        StateMachine.Initialize(_initialState);
    }

    private void Update() {
        StateMachine.Update();
    }


    public void TakeDamage(int damage) {
        // Flash the character when taking damage.
        StartCoroutine(Character.Flash());

        // Reduce the health of the character.
        Health -= damage;

        if (Health <= 0) {
            // Transition to the dead state.
            Character.StateMachine.TransitionToState(Character.DeadState);
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration = 0.1f) {
        // Apply knockback force to the character.
        StartCoroutine(Movement.KnockBack(direction, power, duration));
    }

    public void Die() {
        Destroy(gameObject);
    }
}
