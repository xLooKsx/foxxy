using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement and Jump")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private bool isLookingLeft;
    private bool isWalking;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundLayerRadius;
    private bool isGrounded;

    [Header("Stopm")]
    [SerializeField] private LayerMask stompLayer;
    [SerializeField] private Vector2 stompBoxColliderOffset;
    [SerializeField] private Vector2 stompBoxColliderSize;

    [Header("CoyoteJump")]
    [SerializeField] private float coyoteTimeJump;
    private float currentCoyoteTime;

    [Header("WallSlider")]
    [SerializeField] private float WallSliderDistance;
    [SerializeField] private float WallSliderVelocity;
    [SerializeField] private LayerMask WallSliderLayer;
    private bool isTouchingAWall;

    [Header("WallJump")]
    [SerializeField] private float wallJumpLockTimer;
    [SerializeField] private Vector2 wallJumpForce;
    private int wallJumpDirection;
    private float wallJumpCurrentLockTimer;

    [Header("Interactions")]
    [SerializeField] private IActivalbleStats interaction;

    [Header("JumpBuffer")]
    [SerializeField] private float jumpBufferTimer;
    private float currentJumpBufferTimer;

    [Header("Extra jump")]
    [SerializeField] private int extraJumps;
    [SerializeField] private float maxDistanceFromGround;
    [SerializeField] private float minDistanceForJump;
    private bool hasJumped;
    private int extraJumpsLeft;

    [Header("Components")]
    private Rigidbody2D myRigidBody;
    private Animator animator;

    [Header("Dash")]
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float currentDashTime;

    [Header("Enviroment Metrics")]
    [SerializeField] private GameState currentGameState;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        extraJumpsLeft = extraJumps;
        Core.Instance.GameStateManager.OnGameStateChanged += OnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.GamePlay)
        {
            HandlePlayerInput();    
        }
        ChangePlayerAnimation();
        CheckCoyoteJumpTime();
        CheckBufferJump();
        HandleWallsliderVelocity();
    }

    void FixedUpdate()
    {
        HandlePlayerGroundedStatus();
        HandleStomp();
        CheckWallSliderInteraction();
    }

    private void HandlePlayerGroundedStatus()
    {
        bool wasGrounded = isGrounded;
        Collider2D col = Physics2D.OverlapCircle(transform.position, groundLayerRadius, groundLayer);

        if (col != null)
        {

            if (myRigidBody.linearVelocityY > 0)
            {
                if (col.GetComponent<PlatformEffector2D>() != null)
                {
                    isGrounded = false;
                }
                else
                {
                    isGrounded = true;
                }
            }
            else
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && !wasGrounded)
        {
            extraJumpsLeft = extraJumps;
            hasJumped = false;
        }
        // }

    }

    private void HandleStomp()
    {
        if (!isGrounded && myRigidBody.linearVelocityY < 0)
        {
            Vector2 stompLocation = (Vector2)transform.position + stompBoxColliderOffset;
            Collider2D collider2D = Physics2D.OverlapBox(stompLocation, stompBoxColliderSize, 0, stompLayer);
            if (collider2D != null)
            {
                if (collider2D.TryGetComponent<EnemyHealthManager>(out EnemyHealthManager script))
                {
                    script.HandleDamage(1);
                }
                Jump();
            }

        }
    }

    private void HandlePlayerInput()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isDashing)
        {
            isDashing = true;
            int dashDirection = isLookingLeft ? -1 : 1;
            myRigidBody.linearVelocityX = this.dashForce * dashDirection;   
        }
        

        HandleJumpInput();
        if (wallJumpCurrentLockTimer > 0)
        {
            wallJumpCurrentLockTimer -= Time.deltaTime;
        }
        else if (isDashing)
        {
            if (currentDashTime < dashDuration)
            {
                currentDashTime += Time.deltaTime;
            }
            else
            {
                isDashing = false;
                currentDashTime = 0;
            }
        }
        else if (!isDashing)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            isWalking = horizontalMovement != 0;
            myRigidBody.linearVelocityX = movementSpeed * horizontalMovement;
            DefinePlayerLookDirection(horizontalMovement);
        }

        if (Input.GetKeyDown(KeyCode.F) && this.interaction != null)
        {
            this.interaction.Active();
        }

    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentJumpBufferTimer = jumpBufferTimer;

            if (isTouchingAWall)
            {
                wallJumpCurrentLockTimer = wallJumpLockTimer;
                myRigidBody.linearVelocity = new Vector2(wallJumpDirection * wallJumpForce.x, wallJumpForce.y);
                Flip();
            }
            else if (isGrounded || currentCoyoteTime > 0)
            {
                Jump();
            }
            else if (CheckIfPlayrCanDoExtraJump() && extraJumpsLeft > 0 && hasJumped)
            {
                Jump();
                extraJumpsLeft--;
            }
        }
    }

    void Jump()
    {

        myRigidBody.linearVelocityY = 0;
        myRigidBody.AddForceY(jumpForce, ForceMode2D.Impulse);
        hasJumped = true;
    }

    void CheckCoyoteJumpTime()
    {
        if (isGrounded)
        {
            currentCoyoteTime = coyoteTimeJump;
        }
        else
        {
            currentCoyoteTime -= Time.deltaTime;
        }
    }

    void CheckBufferJump()
    {
        currentJumpBufferTimer -= Time.deltaTime;

        if (currentJumpBufferTimer > 0 && isGrounded)
        {
            Jump();
            currentJumpBufferTimer = 0;
        }
    }

    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void DefinePlayerLookDirection(float horizontalValue)
    {
        if (horizontalValue > 0 && isLookingLeft)
        {
            Flip();
        }
        else if (horizontalValue < 0 && !isLookingLeft)
        {
            Flip();
        }
    }

    void ChangePlayerAnimation()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetFloat("velocityY", myRigidBody.linearVelocityY);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("isWallSlide", isTouchingAWall ? 1 : 0);
        animator.SetBool("isDashing", isDashing);
    }

    void CheckWallSliderInteraction()
    {
        RaycastHit2D rightRayCast = Physics2D.Raycast(transform.position, Vector2.right, WallSliderDistance, WallSliderLayer);
        RaycastHit2D leftRayCast = Physics2D.Raycast(transform.position, Vector2.left, WallSliderDistance, WallSliderLayer);
        bool isHavingAnyCollision = rightRayCast.collider != null || leftRayCast.collider != null;
        wallJumpDirection = rightRayCast.collider != null ? -1 : 1;

        isTouchingAWall = !isGrounded && isHavingAnyCollision;
    }

    void HandleWallsliderVelocity()
    {
        if (isTouchingAWall && myRigidBody.linearVelocityY <= 0)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocityX, Mathf.Max(myRigidBody.linearVelocityY, -WallSliderVelocity));
        }
    }

    bool CheckIfPlayrCanDoExtraJump()
    {
        RaycastHit2D groudHit = Physics2D.Raycast(transform.position, Vector2.down, maxDistanceFromGround, groundLayer);
        if (groudHit.collider != null)
        {
            return groudHit.distance >= minDistanceForJump;
        }
        return false;
    }

    void OnGameStateChanged(GameState newGameState)
    {
        this.currentGameState = newGameState;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, groundLayerRadius);

        Gizmos.color = Color.red;

        Vector2 stompLocation = (Vector2)transform.position + stompBoxColliderOffset;
        Gizmos.DrawWireCube(stompLocation, stompBoxColliderSize);
    }

    public void SetInteraction(IActivalbleStats activalbleStats)
    {
        this.interaction = activalbleStats;
    }
}
