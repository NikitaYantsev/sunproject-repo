using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EnemyMovement : StateMachineBehaviour
{

    public float speed = 2.5f;
    Rigidbody2D body;
    EnemyRotate rotate;
    bool goingForward = true;
    bool inBattle = false;
    public float attackRange = 3f;
    Transform player;
    Vector2 target;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.GetComponent<Rigidbody2D>();
        rotate = animator.GetComponent<EnemyRotate>();
        inBattle = animator.GetComponent<BanditEnemyDetection>().DetectEnemies();
        target = new(body.position.x + 5f, body.position.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!inBattle)
        {
            if (goingForward)
            { 
                while (body.position != target)
                {
                    body.velocity = new(speed * Time.fixedDeltaTime, body.position.y);
                }
                animator.SetTrigger("Idle");
                goingForward = !goingForward;
            }
            else
            {
                while (body.position != target)
                {
                    body.velocity = new(speed * Time.fixedDeltaTime, body.position.y);
                }
                animator.SetTrigger("Idle");
                goingForward = !goingForward;
            }
        }
        else
        {
            rotate.LookAtPlayer();
            Vector2 target = new(player.position.x, body.position.y);
            Vector2 newPos = Vector2.MoveTowards(body.position, target, speed * Time.fixedDeltaTime);
            body.MovePosition(newPos);

            if (Vector2.Distance(player.position, body.position) <= attackRange)
                animator.SetTrigger("Attack");
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}