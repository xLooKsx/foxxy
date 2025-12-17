using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject checkPointBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (checkPointBar.activeSelf)
            {
                Core.Instance.audioManager.PlaySfx(SfxType.Checkpoint);
            }
            checkPointBar.SetActive(false);
            Core.Instance.GameManager.SetCheckPoint(this.transform.position);
            
        }
    }
}
