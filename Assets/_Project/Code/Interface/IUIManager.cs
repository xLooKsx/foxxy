using UnityEngine;

public interface IUIManager
{
    void UpdateExtralives(int lives);
    void UpdateHP(int hp);
    void DisplayLevelName(string levelName);
}
