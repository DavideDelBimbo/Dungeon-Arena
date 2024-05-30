namespace DungeonArena.Interfaces {
    public interface IState  {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}