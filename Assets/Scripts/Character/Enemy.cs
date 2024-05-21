using System;
using UnityEngine;

public enum EnemyState { Wait, Patrol, Chase, Vulnerable }

//[RequireComponent(typeof(Character))]
[RequireComponent(typeof(WaitState))]
[RequireComponent(typeof(PatrolState))]
[RequireComponent(typeof(ChaseState))]
[RequireComponent(typeof(VulnerableState))]
public class Enemy : MonoBehaviour {
    [Header("States")]
    [SerializeField] private EnemyState _initialState = EnemyState.Wait;

    public WaitState WaitState { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public VulnerableState VulnerableState { get; private set; }

    public EnemyBaseState CurrentState { get; private set; }

    public Character Character { get; private set; }

    private void Awake() {
        WaitState = GetComponent<WaitState>();
        PatrolState = GetComponent<PatrolState>();
        ChaseState = GetComponent<ChaseState>();
        VulnerableState = GetComponent<VulnerableState>();

        Character = GetComponent<Character>();
    }

    private void Start() {
        // Transition to the initial state.
        TransitionToState(_initialState);
    }

    private void Update() {
        // Update the current state.
        CurrentState.OnUpdate();
    }
    
    public void TransitionToState(EnemyState nextState) {
        CurrentState?.OnExit();
        CurrentState = GetEnemyState(nextState);
        CurrentState.OnEnter();
    }

    public EnemyBaseState GetEnemyState(EnemyState state) {
        return state switch {
            EnemyState.Wait => WaitState,
            EnemyState.Patrol => PatrolState,
            EnemyState.Chase => ChaseState,
            EnemyState.Vulnerable => VulnerableState,
            _ => throw new ArgumentOutOfRangeException("Invalid enemy state")
        };
    }

}
