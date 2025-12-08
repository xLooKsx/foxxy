using UnityEngine;

public class EnemyMovementA_B : MonoBehaviour
{

    [SerializeField] private Transform enemyTransform;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isLookingLeft;

    private Transform targetPosition;

    void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, targetPosition.position, movementSpeed * Time.deltaTime);
        CheckForNewTargetPosition();
        HandleEnemyFlip();
    }

    private void CheckForNewTargetPosition()
    {
        if (Vector2.Distance(targetPosition.position, enemyTransform.position) < 0.5f)
        {
            targetPosition = targetPosition == pointB ? pointA : pointB;
        }
    }

    private void HandleEnemyFlip()
    {
        if (enemyTransform.position.x < targetPosition.position.x && isLookingLeft)
        {
            Flip();
        }
        else if (enemyTransform.position.x > targetPosition.position.x && !isLookingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        Vector2 currentScale = enemyTransform.localScale;
        currentScale.x *= -1;
        enemyTransform.localScale = currentScale;
    }

    void ResetPosition()
    {
        enemyTransform.position = pointA.position;
        targetPosition = pointB;
        isLookingLeft = true;
    }
}
