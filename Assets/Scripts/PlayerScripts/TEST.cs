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

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        body.freezeRotation = true;
        parryScript = GetComponent<TQTEScript>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

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
            body.velocity = new Vector2(body.velocity.x, jumpHeight);

    }

}
