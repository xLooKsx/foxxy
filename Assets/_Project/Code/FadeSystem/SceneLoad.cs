using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(CallFadeSystem), 0.1f);
    }

    void CallFadeSystem()
    {
        Core.Instance.FadeSystem.Fade();
    }
}
