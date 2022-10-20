using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D body;
    public float moveSpeed = 5f;
    public bool isInFightMode;
    public GameObject Player;
    Vector2 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (isInFightMode)
        
        movementDirection = (Player.transform.position - transform.position).normalized;
        print(movementDirection);
        body.velocity = new Vector2(movementDirection[0] * moveSpeed, body.velocity.y);
        animator.SetBool("IsRunning", body.velocity[0] != 0);
        
    }
}
