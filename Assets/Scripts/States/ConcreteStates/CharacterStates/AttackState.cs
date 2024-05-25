using UnityEngine;

public class AttackState : CharacterState {
    [Header("Attack Settings")]
    [SerializeField] private int _startAttackFrame = 1;
    [SerializeField] private int _attackDurationFrames = 1;

    private bool _hasAttacked;
    private bool _hasPostAttacked;

    
    public override void OnEnter() {
        base.OnEnter();

        // Reset attack flags.
        _hasAttacked = false;
        _hasPostAttacked = false;
    }

    public override void OnUpdate() {
        base.OnUpdate();

        // Attack when the attack frame is reached.
        if (StateAnimation.AnimatedSprite.FrameIndex == _startAttackFrame && !_hasAttacked) {
            // Get the direction of the attack.
            Vector2 direction = _context.StateMachine.ConvertFacingDirectionToVector(_context.StateMachine.CurrentFacingDirection);

            // Attack with the weapon.
            _context.Weapon.Attack(_context.StateMachine.CurrentFacingDirection, direction);
            _hasAttacked = true;
        }

        if (StateAnimation.AnimatedSprite.FrameIndex == (_startAttackFrame + _attackDurationFrames) && !_hasPostAttacked) {
            // Post attack with the weapon.
            _context.Weapon.PostAttack(_context.StateMachine.CurrentFacingDirection);
            _hasPostAttacked = true;
        }

        // Transition to Idle when animation is ended.
        if (!StateAnimation.AnimatedSprite.IsPlaying) {
            _context.StateMachine.TransitionToState(_context.IdleState);
        }
    }
}
