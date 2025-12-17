using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core Instance;

    [Header("Game State")]
    public IGameStateManager GameStateManager {get; private set;} 
    [SerializeField] GameStateManager gameStateManager;
    
    [Header("Game Manager")]
    public IGameManager GameManager {get; private set;}
    [SerializeField] GameManager gameManager;

    [Header("Fade Manager")]
    public IFadeSystem FadeSystem;
    
    [Header("UI Manager")]
    public IUIManager UIManager;  
    
    [Header("UI Manager")]
    public IAudioManager audioManager;
    
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        this.GameStateManager = gameStateManager;
        this.GameManager = gameManager;
    }

    void OnDisable()
    {
        Destroy(this.gameObject);
    }

    public void FadeRegistration(IFadeSystem fadeSystem)
    {
        this.FadeSystem = fadeSystem;
    }
    public void UIRegistration(IUIManager uIManager)
    {
        this.UIManager = uIManager;
    }
    public void IAudioManagerRegistration(IAudioManager audioManager)
    {
        this.audioManager = audioManager;
    }
}
