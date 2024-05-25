using UnityEngine;

public class SceneManager : Singleton<SceneManager> {
    public static void LoadNextScene() {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadScene(string sceneName) {
       UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public static void QuitGame() {
        Application.Quit();
    }
}
