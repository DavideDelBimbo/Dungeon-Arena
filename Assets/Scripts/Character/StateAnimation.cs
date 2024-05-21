using AYellowpaper.SerializedCollections;
using UnityEngine;

[RequireComponent(typeof(AnimatedSprite))]
public class StateAnimation : MonoBehaviour {
    [Header("Animation Settings")]
    [SerializedDictionary("FacingDirection", "Sprite"), SerializeField] private SerializedDictionary<FacingDirection, Sprite[]> _frames;

    private AnimatedSprite _animatedSprite;

    public AnimatedSprite AnimatedSprite { get => _animatedSprite; private set => _animatedSprite = value; }

    void Awake() {
        _animatedSprite = GetComponent<AnimatedSprite>();
    }

    public void Play(FacingDirection direction) {
        _animatedSprite.Frames = _frames[direction];
        _animatedSprite.Play();
    }

    public void Stop() {
        _animatedSprite.Stop();
    }
}