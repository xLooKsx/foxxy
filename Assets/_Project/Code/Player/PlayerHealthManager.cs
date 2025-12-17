using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    [Header("Health Status")]
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    private bool isInvunerable;
    private bool IsDead;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHp = maxHp;
        isInvunerable = false;
        IsDead = false;
        Core.Instance.GameManager.SetCheckPoint(transform.position);
    }

    public void HandleDamage(int value, bool isDead = false)
    {

        if (!isDead)
        {
            if (!isInvunerable)
            {
                currentHp -= value;
                Core.Instance.UIManager.UpdateHP(currentHp);

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
        if (Core.Instance.GameManager.playerHaveExtraLive())
        {            
            StartCoroutine(nameof(DieProcessCoroutine));
        }
        else
        {
            //Game Over
            print("Player morreu T_T");
        }

    }

    IEnumerator DieProcessCoroutine()
    {
        Core.Instance.audioManager.PlaySfx(SfxType.Death);
        Core.Instance.FadeSystem.Fade();
        spriteRenderer.enabled = false;
        Core.Instance.GameStateManager.SetNewGameState(GameState.Fade);
        yield return new WaitUntil(() => Core.Instance.FadeSystem.IsFadeComplete());
        transform.position = Core.Instance.GameManager.GetCheckPoint();
        this.currentHp = this.maxHp;
        Core.Instance.UIManager.UpdateHP(currentHp);
        Core.Instance.GameManager.AddExtraLive(-1);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = true;        
        Core.Instance.FadeSystem.Fade();
        yield return new WaitUntil(() => Core.Instance.FadeSystem.IsFadeComplete());
        Core.Instance.GameStateManager.SetNewGameState(GameState.GamePlay);
        
    }

    IEnumerator ShowPlayerDamageCurotine()
    {
        Core.Instance.audioManager.PlaySfx(SfxType.DamageTaken);
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
