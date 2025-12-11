using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject checkPointBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkPointBar.SetActive(false);
            Core.Instance.GameManager.SetCheckPoint(this.transform.position);
        }
    }
}
