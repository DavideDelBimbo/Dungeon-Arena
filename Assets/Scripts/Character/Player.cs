using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(IInputHandler))]
public class Player : MonoBehaviour {
    public Character Character { get; private set;}
    public Movement Movement { get; private set; }
    public IInputHandler InputHandler { get; private set; }


    private void Awake() {
        Character = GetComponentInChildren<Character>();
        Movement = GetComponent<Movement>();
        InputHandler = GetComponent<IInputHandler>();

        // Dependency injection for the Character.
        Character.Movement = Movement;
        Character.InputHandler = InputHandler;
    }
}
