using UnityEngine;

public abstract class BaseState<T> : MonoBehaviour, IState where T : MonoBehaviour {
    protected T _context;


    protected void Awake() {
        _context = GetComponent<T>();
    }

    protected void Start() {
        enabled = false;
    }


    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void OnUpdate();
}
