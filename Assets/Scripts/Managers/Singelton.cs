using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
	public static T Instance { get; private set; }

    protected virtual void Awake() {
        // Destroy the instance if it already exists.
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this object and don't destroy it on load.
        Instance = (T) this;
        DontDestroyOnLoad(gameObject);
    }
}