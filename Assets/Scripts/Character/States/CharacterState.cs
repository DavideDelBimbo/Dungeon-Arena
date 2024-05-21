using UnityEngine;

public abstract class CharacterState : MonoBehaviour, IState {
    [SerializeField] protected StateAnimation _stateAnimation;

    protected Character character;

    protected virtual void Awake() {
        character = GetComponent<Character>();
    }

    public virtual void OnEnter() {
        _stateAnimation.Play(character.CurrentFacingDirection);
    }

    public virtual void OnUpdate() {}

    public virtual void OnExit() {
        _stateAnimation.Stop();
    }
}
