using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core Instance;
    public IGameStateManager GameStateManager {get; private set;} 
    [SerializeField] GameStateManager gameStateManager;
    public IGameManager GameManager {get; private set;}
    [SerializeField] GameManager gameManager;
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
}
