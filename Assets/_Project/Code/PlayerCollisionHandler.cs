using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{

    private PlayerHealthManager playerHealthManager;

    void Start()
    {
        playerHealthManager = GetComponent<PlayerHealthManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int defaultDamage = 1;
        switch (collision.gameObject.tag)
        {
            case "Danger":
                playerHealthManager.HandleDamage(defaultDamage);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlataform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlataform"))
        {
            this.transform.parent = null;
        }
    }
}
