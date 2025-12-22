using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{

    [Header("Player info")]
    [SerializeField] TMP_Text extraLives;
    [SerializeField] GameObject[] hpHearts;

    [Header("Cutscene")]
    [SerializeField] GameObject blackBars;

    [Header("Level name display")]
    [SerializeField] GameObject levelNamePanelDisplay;
    [SerializeField] TMP_Text levelNameText;



    void Start()
    {
        Core.Instance.UIRegistration(this);
        Core.Instance.GameStateManager.OnGameStateChanged += this.LisenGameStateChange;
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

    private void LisenGameStateChange(GameState newGameState)
    {
        if (GameState.Cutscene.Equals(newGameState))
        {
            this.blackBars.SetActive(true);
        }else{
            this.blackBars.SetActive(false);
        }
    }

    public void DisplayLevelName(string levelName)
    {
        this.levelNameText.text = levelName;
        this.levelNamePanelDisplay.SetActive(true);
    }
}
