using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamage
{

    [Header("HealthParam")]
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;

    [Header("Components")]
    [SerializeField] private GameObject rootObject;

    void Start()
    {
        currentHp = maxHp;
        Core.Instance.GameStateManager.OnGameStateChanged += this.ResetPosition;
    }

    public void HandleDamage(int value)
    {
        currentHp -= value;

        if (currentHp <= 0)
        {
            HandleDeath();
        }
        else
        {
            Core.Instance.audioManager.PlaySfx(SfxType.DamageTaken);
        }
    }

    private void HandleDeath()
    {
        Core.Instance.audioManager.PlaySfx(SfxType.Death);
        rootObject.SetActive(false);
    }

    public void ResetPosition(GameState gameState)
    {
        if (GameState.Fade.Equals(gameState))
        {
            if (!rootObject.gameObject.activeSelf)
            {
                rootObject.SetActive(true);                                
            }
            this.currentHp = maxHp;
        }
    }
}
