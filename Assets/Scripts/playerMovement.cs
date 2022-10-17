using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 7f;
    [SerializeField] public float jumpHeight = 13f;
    [SerializeField] public float rollDistance = 7f;
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
        if (Input.GetKeyDown(KeyCode.Space)) // grounded
        {
            Jump();
        }

        
        //Roll
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded()) // grounded 
        {
            StartCoroutine(Roll());
        }

        //Set animator parameters
        print(isGrounded());
        animator.SetBool("isRunning", horizontalInput != 0);
        animator.SetBool("isGrounded", isGrounded()); // grounded
        animator.SetBool("isClimbing", isClimbing());
    }

    private void FixedUpdate()
    {
        if (isClimbing())
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    void Jump() // IN PROGRESS
    {
        if (isGrounded() && !isRolling && !onWall())
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetTrigger("jump");
        }
        else if (isClimbing()) 
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }

    }

    IEnumerator Roll()
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

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.2f, wallLayer);
        return raycastHit.collider != null;
    }

    private bool isClimbing()
    {
        return !isGrounded() && onWall();
    }
}