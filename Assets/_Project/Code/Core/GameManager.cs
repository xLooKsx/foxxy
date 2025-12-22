using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] int extraLives;
    [SerializeField] int currentGems;
    [SerializeField] Vector3 checkPoint;
    [SerializeField] string levelName;

    void Start()
    {
        Invoke(nameof(UpdateUIOnStartup), 0.1f);
        Invoke(nameof(DisplayLevelName), 0.2f);
    }

    private void DisplayLevelName()
    {
        Core.Instance.UIManager.DisplayLevelName(levelName);
    }

    void UpdateUIOnStartup()
    {
        Core.Instance.UIManager.UpdateExtralives(extraLives);
    }

    public void AddExtraLive(int quantity)
    {
        extraLives += quantity;
        Core.Instance.UIManager.UpdateExtralives(extraLives);
    }

    public void AddGem()
    {
        currentGems++;
    }

    public Vector3 GetCheckPoint()
    {
        return checkPoint;
    }

    public bool playerHaveExtraLive()
    {
        return extraLives >= 0;
    }

    public void SetCheckPoint(Vector3 checkPoint)
    {
        this.checkPoint = checkPoint;
    }
}
