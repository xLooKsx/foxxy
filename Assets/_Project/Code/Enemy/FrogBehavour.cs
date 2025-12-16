using System.Collections;
using UnityEngine;

public class FrogBehavour : MonoBehaviour
{

    [SerializeField] private Transform frog;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float jumpDuration;
    [SerializeField] private float arcHeight;
    [SerializeField] private float delayBetweenJumps;
    [SerializeField] private bool isLookingLeft;
    [SerializeField] private Animator animator;
    private bool goingB = true;

    void Start()
    {        
        StartCoroutine(nameof(JumpCoroutine));
    }

    IEnumerator JumpCoroutine()
    {
        while (true)
        {
            SetGroundedAnimation(true);
            yield return new WaitForSeconds(delayBetweenJumps);

            Vector3 startPosition = goingB ? pointA.position : pointB.position;
            Vector3 endPosition = goingB ? pointB.position : pointA.position;

            yield return JumpArcCoroutine(startPosition, endPosition);
            goingB = !goingB;
        }
    }

    IEnumerator JumpArcCoroutine(Vector3 start, Vector3 end)
    {
        if(end.x < frog.position.x && !isLookingLeft)
        {
            Flip();
        } else if(end.x > frog.position.x && isLookingLeft)
        {
            Flip();
        }

        float t = 0;
        while(t < 1)
        {
            SetGroundedAnimation(false);
            t += Time.deltaTime / jumpDuration;
            SetJumpAndFallAnmation(t > 0.5f ? -1 : 1);
            float height = Mathf.Sin(Mathf.PI * t) * arcHeight;
            frog.position = Vector3.Lerp(start, end, t) + Vector3.up * height;

            yield return new WaitForEndOfFrame();
        }

        frog.position = end;
    }

    void SetGroundedAnimation(bool isGrounded)
    {
        animator.SetBool("IsGrounded", isGrounded);
    }

        void SetJumpAndFallAnmation(int isGrounded)
    {
        animator.SetFloat("Velocity", isGrounded);
    }


    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        Vector2 currentScale = frog.localScale;
        currentScale.x *= -1;
        frog.localScale = currentScale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        const int resolution = 20;
        
        for (int i = 0; i < resolution; i++)
        {
            float t1 = i / (float) resolution;
            float t2 = (i+1) / (float) resolution;

            Vector3 pos1 = Vector3.Lerp(pointA.position, pointB.position, t1) + Vector3.up * Mathf.Sin(Mathf.PI * t1) * arcHeight;
            Vector3 pos2 = Vector3.Lerp(pointA.position, pointB.position, t2) + Vector3.up * Mathf.Sin(Mathf.PI * t2) * arcHeight;
            Gizmos.DrawLine(pos1, pos2);
        }
    }
}
