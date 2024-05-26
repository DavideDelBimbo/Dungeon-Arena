using System.Collections;
using UnityEngine;

public class SceneManager : Singleton<SceneManager> {
    private float progress = 0f;

    public float Progress => progress;

    public bool IsLoaded => progress == 1f;


    public void LoadNextScene() {
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(Loading(currentScene + 1));
    }

    public void LoadScene(string sceneName) {
       UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public static void QuitGame() {
        Application.Quit();
    }


    private IEnumerator Loading(int index) {
        // Loads the scene in the background.
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);

        // Wait the last operation fully loads.
        while (!asyncLoad.isDone) {
            progress = asyncLoad.progress;
            yield return null;
        }

        progress = 1f;
        yield break;
    }
}
