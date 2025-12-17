using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamage
{

    [Header("HealthParam")]
    [SerializeField] private int maxHp;
    private int currentHp;

    [Header("Components")]
    [SerializeField] private GameObject rootObject;

    void Start()
    {
        currentHp = maxHp;
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
}
