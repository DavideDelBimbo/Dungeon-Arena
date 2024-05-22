using AYellowpaper.SerializedCollections;
using UnityEngine;
using static Character;

[RequireComponent(typeof(AnimatedSprite))]
public class StateAnimation : MonoBehaviour {
    [Header("Animation Settings")]
    [SerializedDictionary("FacingDirection", "Sprite"), SerializeField] private SerializedDictionary<FacingDirection, Sprite[]> _frames;

    private AnimatedSprite _animatedSprite;


    public AnimatedSprite AnimatedSprite {
        get => _animatedSprite;
        private set => _animatedSprite = value;
    }


    void Awake() {
        AnimatedSprite = GetComponent<AnimatedSprite>();
    }

    public void Play(FacingDirection direction) {
        AnimatedSprite.Frames = _frames[direction];
        AnimatedSprite.Play();
    }

    public void Stop() {
        AnimatedSprite.Stop();
    }
}