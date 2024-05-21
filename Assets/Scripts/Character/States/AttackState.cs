public class AttackState : CharacterState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Transition to idle when animation is ended
        if (!_stateAnimation.AnimatedSprite.IsPlaying) {
            character.TransitionToState(State.Idle);
        }
    }
}
