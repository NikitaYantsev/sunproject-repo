using System.Collections;
using UnityEngine;
//GITHUB TEST
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float jumpHeight = 13f;
    [SerializeField] float rollDistance = 7f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] float wallSlidingSpeed = 0.8f;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    bool isRolling;
    float horizontalInput;
    
    // private bool grounded;
    void Awake()
    {
        //Grab components from other objects
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.freezeRotation = true;
    }
    
    private void Update()
    {
        //Horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        if (!isRolling)
        {
            body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);
        }

        //Rotate character when moving
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        
        //Roll
        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded()) 
        {
            StartCoroutine(Roll());
        }

        //Set animator parameters
        animator.SetBool("isRunning", horizontalInput != 0);
        animator.SetBool("isGrounded", IsGrounded()); 
        animator.SetBool("isClimbing", IsClimbing());
    }

    private void FixedUpdate()
    {
        if (IsClimbing())
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    void Jump() // IN PROGRESS
    {
        if (IsGrounded() && !isRolling && !OnWall())
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetTrigger("jump");
        }
        else if (IsClimbing()) 
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }

    }

    IEnumerator Roll() //DOESN'T WORK CORRECTLY WHILE SPAMMING LSHIFT, CAN ROLL WHILE ATTACKING
    {
        isRolling = true;
        animator.SetBool("isRolling", isRolling);
        animator.SetTrigger("roll");
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(new Vector2(rollDistance * transform.localScale.x, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("roll");
        isRolling = false;
        animator.SetBool("isRolling", isRolling);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.2f, wallLayer);
        return raycastHit.collider != null;
    }

    private bool IsClimbing()
    {
        return !IsGrounded() && OnWall();
    }
}