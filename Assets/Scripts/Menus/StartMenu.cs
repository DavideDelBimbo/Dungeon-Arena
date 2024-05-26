using UnityEngine;

public class StartMenu : MonoBehaviour {
    public void StartGame() {
        // Load the character selection scene.
        SceneManager.Instance.LoadNextScene();
    }

    public void QuitGame() {
        SceneManager.QuitGame();
    }
}
