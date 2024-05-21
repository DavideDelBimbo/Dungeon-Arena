using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyBaseState : MonoBehaviour, IState {
    protected Enemy _enemy;
    protected AIInputHandler InputHandler => (AIInputHandler) _enemy.Character.InputHandler;

    protected virtual void Awake() {
        _enemy = GetComponent<Enemy>();
    }

    public virtual void OnEnter() {}

    public virtual void OnUpdate() {}

    public virtual void OnExit() {}
}
