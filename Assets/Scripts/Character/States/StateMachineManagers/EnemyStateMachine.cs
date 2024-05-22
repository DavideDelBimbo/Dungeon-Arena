using System.ComponentModel;
using static Enemy;

public class EnemyStateMachine : StateMachine<Enemy, EnemyState> {
    public EnemyStateMachine(Enemy context) : base(context) { }


    public void Initialize(State initialState) {
        // Transition to the initial state.
        EnemyState intialEnemyState = ConvertStateToEnemyState(initialState);
        TransitionToState(intialEnemyState);
    }


    // Convert the state value to the enemy state object.
    public EnemyState ConvertStateToEnemyState(State state) => state switch {
        State.Wait => _context.WaitState,
        State.Patrol => _context.PatrolState,
        State.Chase => _context.ChaseState,
        State.Vulnerable => _context.VulnerableState,
        _ => throw new InvalidEnumArgumentException($"Invalid enemy state value: {state}.")
    };
}
