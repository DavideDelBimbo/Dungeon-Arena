using System.Collections;
using UnityEngine;

namespace DungeonArena.States.CharacterStates {
    public class AttackState : CharacterState {
        [Header("Attack Settings")]
        [SerializeField] private int _startAttackFrame = 1;
        [SerializeField] private int _attackDurationFrames = 1;
        [SerializeField] private float _attackRate = 1.0f;

        private bool _hasAttacked;
        private bool _hasPostAttacked;
        
        public bool CanAttack { get; private set; } = true;
        
        
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
                StartCoroutine(AttackCooldown());
                _context.StateMachine.TransitionToState(_context.IdleState);
            }
        }

        private IEnumerator AttackCooldown() {
            CanAttack = false;
            yield return new WaitForSeconds(_attackRate);
            CanAttack = true;
        }
    }
}