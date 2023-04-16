using System.Collections;
using UnityEngine;
public class TCharControls : MonoBehaviour
{
    public float horizontalInput;

    public float moveSpeed = 7f;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator animator;
    private ParryScript parryScript;

    void Start()
    {
        //Grab components from other objects
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        parryScript = GetComponent<ParryScript>();
        body.freezeRotation = true;
    }

    private void Update()
    {
        //Horizontal movement
        //������� ������� �������
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        //Rotate character when moving
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);

        if (Input.GetKeyDown(KeyCode.F))
        {
            body.velocity = new Vector2(0, 0);
            horizontalInput = 0;
            parryScript.CheckAndParryOne();
        }

        animator.SetBool("isRunning", horizontalInput != 0);
    }
}