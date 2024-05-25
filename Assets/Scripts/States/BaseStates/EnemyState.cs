using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyState : BaseState<Enemy> {
    protected AIInputHandler InputHandler => (AIInputHandler) _context.InputHandler;


    public override void OnEnter() { }

    public override void OnUpdate() { }

    public override void OnExit() { }
}