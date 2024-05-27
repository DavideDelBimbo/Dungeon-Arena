using UnityEngine;

public abstract class ItemEffect : MonoBehaviour {
    public abstract void ApplyEffect(IAgent target);
}