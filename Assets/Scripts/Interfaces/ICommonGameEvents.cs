using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This interface allows a prefab's root GameObject to find all child
/// GameObjects and notify them of common game events such as when the game is
/// paused, when the player starts a level, etc. That's at least 4 - 6 GELs 
/// removed from each child GameObject
/// </summary>
/// 
public interface ICommonGameEvents {
    void GamePaused(bool paused);
    void GameOver();
    void LevelStarted();
    void LevelCompleted();
}
