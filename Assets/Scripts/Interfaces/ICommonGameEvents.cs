namespace cpioli.Events
{
    /// <summary>
    /// This interface allows a prefab's root's GameObject to find all child
    /// GameObjects and notify them of common game events such as when the game is
    /// paused, when the player starts a level, etc. That's at least 4 - 6 GELs 
    /// removed from each child GameObject
    /// </summary>
    public interface ICommonGameEvents
    {
        void GamePaused();
        void GameResumed();
        void GameOver();
        void LevelStarted();
        void LevelCompleted();
    }
}
