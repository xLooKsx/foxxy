using UnityEngine;

public class CrankBehavior : MonoBehaviour, IActivalbleStats
{

    public GameObject target;
    public void Active()
    {
        if(target.TryGetComponent<IActivalbleStats>(out IActivalbleStats output))
        {
            output.Active();
        }
    }
}
