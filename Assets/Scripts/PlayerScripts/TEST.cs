using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    float moveSpeed = 5f;
    float horizontalInput;
    private Rigidbody2D body;
    float jumpHeight = 15f;
    TQTEScript parryScript;
    Animator animator;
    [SerializeField] float rollDistance = 7f;
    [SerializeField] float sprintSpeed;
    [SerializeField] LayerMask groundLayer;
    private BoxCollider2D boxCollider;
    float shiftCountdown;
    bool isRolling;
    [SerializeField] PlayerStamina stamina;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.freezeRotation = true;
        parryScript = GetComponent<TQTEScript>();
        stamina = GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

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
            if (IsGrounded() && !isRolling && stamina.currentStamina > 2.5f) //and current state is suitable for rolling, do roll
                StartCoroutine(Roll());
        }
        else
        {
            animator.SetBool("Sprint", false); //when Shift get released stop sprinting (in any case) and reset coundown
            shiftCountdown = 0;
        }
        
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);

        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("startedRunning");
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            animator.SetTrigger("stoppedRunning");
        }

        //Parry


        animator.SetBool("isRunning", horizontalInput != 0);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        animator.SetBool("isGrounded", IsGrounded()); 
        
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }
    
    IEnumerator Roll()
    {
        isRolling = true;
        animator.SetBool("isRolling", isRolling);

        body.velocity = new Vector2(0f, 0f);
        body.AddForce(new Vector2(rollDistance * transform.localScale.x, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);

        isRolling = false;
        animator.SetBool("isRolling", isRolling);
    }
    
    void Jump() 
    {
        if (IsGrounded() && !isRolling)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetTrigger("jump");
        }
    }
}
