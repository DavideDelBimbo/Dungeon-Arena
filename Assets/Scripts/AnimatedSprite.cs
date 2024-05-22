using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour {
    [Header("Animation Settings")]
    [SerializeField] private Sprite[] _frames;
    [SerializeField] private int _startFrameIndex = 0;
    [SerializeField] private float _refreshTime = 0.2f;
    [SerializeField] private bool _loop = true;

    private int _frameIndex;
    public SpriteRenderer _spriteRenderer;
    private Coroutine _animationCoroutine;


    public Sprite[] Frames {
        get => _frames;
        set => _frames = value;
    }

    public int FrameIndex {
        get => _frameIndex;
        private set => _frameIndex = value % Frames.Length;
    }

    public bool IsPlaying => _animationCoroutine != null;
    private bool IsLastFrame => FrameIndex == (Frames.Length + _startFrameIndex) % Frames.Length;


    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void Play() {
        if (IsPlaying) return;

        // Set the frame index to the start frame.
        FrameIndex = _startFrameIndex;
        _spriteRenderer.sprite = Frames[FrameIndex];

        // Start the animation coroutine.
        _spriteRenderer.enabled = true;
        _animationCoroutine = StartCoroutine(Animate());
    }

    public void Stop() {
        if (!IsPlaying) return;

        // Stop the animation coroutine.
        StopCoroutine(_animationCoroutine);
        _animationCoroutine = null;
        _spriteRenderer.enabled = false;

        // Reset the frame index to the start frame.
        FrameIndex = _startFrameIndex;
        _spriteRenderer.sprite = Frames[FrameIndex];
    }


    // Set the next frame of the animation.
    private IEnumerator Animate() {
        while (true) {
            // Wait for the refresh time.
            yield return new WaitForSeconds(_refreshTime);

            // Get the next frame.
            FrameIndex++;
            _spriteRenderer.sprite = Frames[_frameIndex];

            // Stop the animation if it's not looping and it's the last frame.
            if (!_loop && IsLastFrame) {
                Stop();
                yield break;
            }
        }
    }
}
