using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] int extraLives;
    [SerializeField] int currentGems;
    [SerializeField] Vector3 checkPoint;

    public void AddExtraLive(int quantity)
    {
        extraLives += quantity;
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
        return extraLives > 0;
    }

    public void SetCheckPoint(Vector3 checkPoint)
    {
        this.checkPoint = checkPoint;
    }
}
