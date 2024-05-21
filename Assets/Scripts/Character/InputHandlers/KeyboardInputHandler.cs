using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour, IInputHandler {
    public Vector2 GetMovement() {
        Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Avoid diagonal movement.
        if (input.x != 0) {
            input.y = 0;
        }

        return input;
    }

    public bool GetFire() {
        return Input.GetButton("Fire1");
    }
}
