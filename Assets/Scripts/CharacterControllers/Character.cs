using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonArena.Interfaces;
using DungeonArena.States.StateMachines;
using DungeonArena.States.CharacterStates;

namespace DungeonArena.CharacterControllers {

    [RequireComponent(typeof(IdleState))]
    [RequireComponent(typeof(WalkState))]
    [RequireComponent(typeof(AttackState))]
    [RequireComponent(typeof(DeadState))]
    public class Character : MonoBehaviour {
        public enum State { Idle, Walk, Attack, Dead }
        public enum FacingDirection { Up, Down, Left, Right }   


        [Header("Character Settings")]
        [SerializeField] private Sprite _previewSprite;
        [SerializeField] private SpriteRenderer _shadowSprite;

        [Header("States")]
        [SerializeField] private State _initialState = State.Idle;
        [SerializeField] private FacingDirection _initialFacingDirection = FacingDirection.Down;


        public Sprite PreviewSprite => _previewSprite;
        public SpriteRenderer ShadowSprite => _shadowSprite;

        public IAgent Agent { get; set; }
        public Movement Movement { get; set; }
        public IInputHandler InputHandler { get; set; }

        public IWeapon Weapon { get; set; }

        public CharacterStateMachine StateMachine { get; private set; }
        public IdleState IdleState { get; private set; }
        public WalkState WalkState { get; private set; }
        public AttackState AttackState { get; private set; }
        public DeadState DeadState { get; private set; }


        private void Awake() {
            Agent ??= GetComponentInParent<IAgent>();
            Movement = (Movement != null) ? Movement : GetComponent<Movement>();
            InputHandler ??= GetComponent<IInputHandler>();
        
            Weapon ??= GetComponentInChildren<IWeapon>();

            StateMachine = new CharacterStateMachine(this);
            IdleState = GetComponent<IdleState>();
            WalkState = GetComponent<WalkState>();
            AttackState = GetComponent<AttackState>();
            DeadState = GetComponent<DeadState>();
        }

        private void Start() {
            StateMachine.Initialize(_initialState, _initialFacingDirection);
        }

        private void Update() {
            StateMachine.Update();
        }


        // Flash the character sprite changing the color.
        public IEnumerator Flash(Color color, float duration = 0.1f, int times = 1) {
            // Get all the sprite renderers of the character.
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < times; i++) {
                // Flash the character sprite changing the color.
                Array.ForEach(spriteRenderers, spriteRenderer => spriteRenderer.color = color);
                yield return new WaitForSeconds(duration);

                // Reset the character sprite color.
                Array.ForEach(spriteRenderers, spriteRenderer => spriteRenderer.color = Color.white);
                yield return new WaitForSeconds(duration);
            }

            yield break;
        }

        // Flash the character sprite changing the material.
        public IEnumerator Flash(Material material, float duration = 0.1f, int times = 1) {
            // Get all the sprite renderers of the character.
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            // Store the original materials of the character sprite.
            Dictionary<SpriteRenderer, Material> originalMaterials = new();
            Array.ForEach(spriteRenderers, spriteRenderer => originalMaterials[spriteRenderer] = spriteRenderer.material);

            for (int i = 0; i < times; i++) {
                // Flash the character sprite changing the material.
                Array.ForEach(spriteRenderers, spriteRenderer => spriteRenderer.material = material);
                yield return new WaitForSeconds(duration);

                // Reset the character sprite material.
                Array.ForEach(spriteRenderers, spriteRenderer => spriteRenderer.material = originalMaterials[spriteRenderer]);
                yield return new WaitForSeconds(duration);
            }

            yield break;
        }
    }
}