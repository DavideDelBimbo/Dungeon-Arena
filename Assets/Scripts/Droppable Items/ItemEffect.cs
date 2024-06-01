using UnityEngine;
using DungeonArena.Interfaces;

namespace DungeonArena.DroppableItems {
    public abstract class ItemEffect : MonoBehaviour {
        public abstract void ApplyEffect(IAgent target);
    }
}