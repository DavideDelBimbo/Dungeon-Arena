using UnityEngine;

public class SceneManager : Singleton<SceneManager> {
    public static void LoadScene(string sceneName) {
       UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
