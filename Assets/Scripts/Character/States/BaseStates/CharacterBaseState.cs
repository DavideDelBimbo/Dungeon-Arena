using UnityEngine;

[RequireComponent(typeof(Character))]
public abstract class CharacterBaseState : MonoBehaviour, IState {
    [Header("State Settings")]
    [SerializeField] protected StateAnimation _stateAnimation;

    protected Character _character;

    protected virtual void Awake() {
        _character = GetComponent<Character>();
    }

    public virtual void OnEnter() {
        _stateAnimation.Play(_character.CurrentFacingDirection);
    }

    public virtual void OnUpdate() {}

    public virtual void OnExit() {
        _stateAnimation.Stop();
    }
}
