using DungeonArena.Utils;
using UnityEngine;

namespace DungeonArena.Objects {

    [RequireComponent(typeof(AnimatedSprite))]
    public class Fire : MonoBehaviour {
        private AnimatedSprite _animatedSprite;

        private void Awake() {
            _animatedSprite = GetComponent<AnimatedSprite>();
        }

        private void Start() {
            _animatedSprite.Play();
        }
    }
}