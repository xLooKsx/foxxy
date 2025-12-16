using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{

    [SerializeField] TMP_Text extraLives;
    [SerializeField] GameObject[] hpHearts;

    void Start()
    {
        Core.Instance.UIRegistration(this);        
    }

    public void UpdateExtralives(int lives)
    {
        extraLives.text = "X " + lives.ToString();
    }

    public void UpdateHP(int hp)
    {
        foreach(GameObject heart in hpHearts)
        {
            heart.SetActive(false);
        }

        int lefthearts = Mathf.Clamp(hp, 0, hpHearts.Length);
        for(int i=0; i < lefthearts; i++)
        {
            hpHearts[i].SetActive(true);
        }
    }
}
