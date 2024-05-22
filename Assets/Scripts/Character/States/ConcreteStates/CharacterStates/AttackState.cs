public class AttackState : CharacterState {
    public override void OnUpdate() {
        base.OnUpdate();

        // Transition to Idle when animation is ended.
        if (!StateAnimation.AnimatedSprite.IsPlaying) {
            _context.StateMachine.TransitionToState(_context.IdleState);
        }
    }
}
