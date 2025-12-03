using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
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
    }

    private void HandleDeath()
    {
        rootObject.SetActive(false);
    }
}
