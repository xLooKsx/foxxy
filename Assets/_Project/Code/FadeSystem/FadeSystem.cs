using UnityEngine;

public class FadeSystem : MonoBehaviour, IFadeSystem
{

    [SerializeField] GameObject fadePanel;
    [SerializeField] Animator animator;
    private bool fadeComplete = true;

    void Awake()
    {
        fadePanel.SetActive(true);
    }

    void Start()
    {
        Core.Instance.FadeRegistration(this);
    }

    public void Fade()
    {
        if (fadeComplete)
        {
            fadeComplete = false;
            animator.SetTrigger("Fade");
        }
    }

    public bool IsFadeComplete()
    {
        return fadeComplete;
    }

    public void SetFadeComplete()
    {
        fadeComplete = true;
    }
}
