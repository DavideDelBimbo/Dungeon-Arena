using System.Collections;
using UnityEngine;

namespace DungeonArena.Managers {
    public class SceneManager : Singleton<SceneManager> {
        private float progress = 0f;

        public float Progress => progress;

        public bool IsLoaded => progress == 1f;


        public void LoadNextScene() {
            int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(Loading(currentScene + 1));
        }

        public void LoadScene(string sceneName) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void LoadScene(int index) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
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
}