using UnityEngine;

public interface IGameManager
{
    void AddGem();
    bool playerHaveExtraLive();
    void AddExtraLive(int quantity);

    void SetCheckPoint(Vector3 checkPoint);

    Vector3 GetCheckPoint();
}
