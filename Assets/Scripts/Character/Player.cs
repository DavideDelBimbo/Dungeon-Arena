using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(IInputHandler))]
public class Player : MonoBehaviour, IAgent, IDamageable {
    public int Health { get; set; }

    public Character Character { get; private set;}
    public Movement Movement { get; private set; }
    public IInputHandler InputHandler { get; private set; }


    private void Awake() {
        Character = GetComponentInChildren<Character>();
        Movement = GetComponent<Movement>();
        InputHandler = GetComponent<IInputHandler>();

        // Dependency injection for the Character.
        Character.Agent = this;
        Character.Movement = Movement;
        Character.InputHandler = InputHandler;
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
        // Destroy the player game object.
        Destroy(gameObject);

        // Show the game over screen.
        Invoke(nameof(GameManager.Instance.EndGame), 1f);
    }
}
