using System.Collections;
using UnityEngine;

public class MovingWall : MonoBehaviour, IActivalbleStats
{

    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Transform finalPosition;
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject cutsceneCamera;
    private bool isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetObject.position = initialPosition.position;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            targetObject.position = Vector3.MoveTowards(targetObject.position, finalPosition.position, movementSpeed * Time.deltaTime);
        }
    }

    public void Active()
    {
        this.isActive = true;
        Core.Instance.GameStateManager.SetNewGameState(GameState.Cutscene);
        if(this.cutsceneCamera != null)
        {
            StartCoroutine(nameof(CutsceneCorroutine));    
        }
        
    }

    IEnumerator CutsceneCorroutine()
    {
        this.cutsceneCamera.SetActive(true);
        yield return new WaitUntil(() => this.targetObject.position == this.finalPosition.position);
        this.cutsceneCamera.SetActive(false);
        Core.Instance.GameStateManager.SetNewGameState(GameState.GamePlay);
    }
}
