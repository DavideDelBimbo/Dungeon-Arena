public class AttackState : CharacterBaseState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Transition to Idle when animation is ended
        if (!_stateAnimation.AnimatedSprite.IsPlaying) {
            _character.TransitionToState(CharacterState.Idle);
        }
    }
}
