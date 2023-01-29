using System.Collections;
using UnityEngine;
public class PlayerMovement: MonoBehaviour
{
    float horizontalInput;

    public float moveSpeed = 7f;
    [SerializeField] float jumpHeight = 13f;
    [SerializeField] float rollDistance = 7f;
    [SerializeField] float wallSlidingSpeed = 0.8f;
    [SerializeField] float sprintSpeed;
    float shiftCountdown;
    //public float wallJumpDuration;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;

    private PlayerStamina stamina;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private PlayerInteraction interactionScript;
    private QTEScript parryScript;

    bool isRolling;


    // private bool grounded;
    void Start()
    {
        //Grab components from other objects
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        stamina = GetComponent<PlayerStamina>();
        interactionScript = GetComponent<PlayerInteraction>();
        parryScript = GetComponent<QTEScript>();
        body.freezeRotation = true;

        transform.position = GameObject.FindGameObjectWithTag("startPos").transform.position;
    }
    
    private void Update()
    {
        //Horizontal movement
        //Сделать поворот быстрее
        horizontalInput = Input.GetAxis("Horizontal");
        if (!isRolling)
            body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        //Actions with LShift pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftCountdown += Time.deltaTime; //timer to detect how long LShift is pressed
            if (!isRolling && IsGrounded() && shiftCountdown > 0.3f && stamina.currentStamina > 0) //if LShift is pressed longer than 0.3 sec, sprint
            {
                body.velocity = new Vector2(horizontalInput * sprintSpeed, body.velocity.y);
                animator.SetBool("Sprint", true);
            }

                
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && shiftCountdown < 0.3f) //if LShift is pressed longer than 0.3 sec
        {
            if (IsGrounded() && !isRolling && stamina.currentStamina > 2.5f) //and current state is suitable for rolling, sprint
                StartCoroutine(Roll());
        }
        else
        {
            animator.SetBool("Sprint", false);
            shiftCountdown = 0;
        }
            

        //Rotate character when moving
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);

        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1.7f, 1.7f, 1.7f);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();


        //Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactionScript.Interact();
        }

        //Parry
        if (Input.GetKeyDown(KeyCode.F) && animator.GetBool("isGrounded"))
        {
            body.velocity = new Vector2(0, 0);
            horizontalInput = 0;
            animator.SetBool("Sprint", false);
            parryScript.StartQTE("PlayerParry", 1f);            
        }

        //Set animator parameters
        animator.SetBool("isRunning", horizontalInput != 0);
        animator.SetBool("isGrounded", IsGrounded()); 
        animator.SetBool("isClimbing", IsClimbing());
    }

    private void FixedUpdate()
    {
        if (IsClimbing())
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }

    void Jump() 
    {
        if (IsGrounded() && !isRolling)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetTrigger("jump");
        }
        else if (IsClimbing())  // IN PROGRESS
        {
            if (horizontalInput == 0)
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }
    }

    IEnumerator Roll() //CAN ROLL WHILE ATTACKING
    {
        isRolling = true;
        animator.SetBool("isRolling", isRolling);

        body.velocity = new Vector2(0f, 0f);
        body.AddForce(new Vector2(rollDistance * transform.localScale.x, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);

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