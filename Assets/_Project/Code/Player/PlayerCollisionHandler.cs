using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{

    private PlayerHealthManager playerHealthManager;
    private PlayerController player;
    [SerializeField] IActivalbleStats interaction;

    void Start()
    {
        playerHealthManager = GetComponent<PlayerHealthManager>();
        player = GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int defaultDamage = 1;
        switch (collision.gameObject.tag)
        {
            case "Danger":
                playerHealthManager.HandleDamage(defaultDamage);
                break;

            case "Interaction":
                if(collision.gameObject.TryGetComponent<IActivalbleStats>(out IActivalbleStats output)){
                    player.SetInteraction(output);
                }
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {

            case "Interaction":
                player.SetInteraction(null);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlataform"))
        {
            this.transform.parent = collision.transform;
            player.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlataform"))
        {
            this.transform.parent = null;
            player.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
