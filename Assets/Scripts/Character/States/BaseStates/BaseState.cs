using UnityEngine;

public abstract class BaseState<T> : MonoBehaviour, IState where T : MonoBehaviour {
    protected T _context;


    protected void Awake() {
        _context = GetComponent<T>();
    }

    public virtual void OnEnter() { }

    public virtual void OnExit() { }

    public virtual void OnUpdate() { }
}
