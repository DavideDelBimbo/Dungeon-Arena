using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(IInputHandler))]
[RequireComponent(typeof(WaitState))]
[RequireComponent(typeof(PatrolState))]
[RequireComponent(typeof(ChaseState))]
[RequireComponent(typeof(VulnerableState))]
public class Enemy : MonoBehaviour {
    public enum State { Wait, Patrol, Chase, Vulnerable }


    [Header("Enemy Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _targetLayer;

    [Header("States")]
    [SerializeField] private State _initialState = State.Wait;


    public Character Character { get; private set; }
    public Movement Movement { get; private set; }
    public IInputHandler InputHandler { get; private set; }

    public Transform Target { get => _target; private set => _target = value; }
    public LayerMask TargetLayer { get => _targetLayer; private set => _targetLayer = value; }

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
}
