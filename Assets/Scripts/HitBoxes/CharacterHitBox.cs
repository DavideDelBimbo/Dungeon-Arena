using DungeonArena.Interfaces;

namespace DungeonArena.HitBoxes {
    public class CharacterHitBox : HitBox { 
        public IAgent Agent => GetComponentInParent<IAgent>();
    }
}