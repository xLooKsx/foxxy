using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    [Header("Health Status")]
    [SerializeField] private int maxHp;
    private int currentHp;
    private bool isInvunerable;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHp = maxHp;
        isInvunerable = false;
    }

    public void HandleDamage(int value, bool isDead = false)
    {

        if (!isDead)
        {
            if (!isInvunerable)
            {
                currentHp -= value;

                if (currentHp <= 0)
                {
                    HandlePlayerDeath();
                }
                else
                {
                    StartCoroutine(nameof(ShowPlayerDamageCurotine));
                }
            }

        }
        else
        {
            HandlePlayerDeath();
        }


    }

    private void HandlePlayerDeath()
    {
        print("Player is dead, sad...");
    }

    IEnumerator ShowPlayerDamageCurotine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        StartCoroutine(nameof(HandlePlayerInvunerabilityTime));
    }

    IEnumerator HandlePlayerInvunerabilityTime()
    {
        for (int i = 0; i < 8; i++)
        {
            isInvunerable = true;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }

        isInvunerable = false;
        spriteRenderer.enabled = true;
    }
}
