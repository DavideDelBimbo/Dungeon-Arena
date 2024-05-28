using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyState : BaseState<Enemy> {
    protected AIInputHandler InputHandler => (AIInputHandler) _context.InputHandler;


    public override void OnEnter() {
        enabled = true;
    }

    public override void OnUpdate() { }

    public override void OnExit() {
        enabled = false;
    }
}