using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyMovement : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float attackRange = 3f;

    UnityEngine.Transform player;
    Rigidbody2D body;
    EnemyRotate rotate;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rotate.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, body.position.y);
        Vector2 newPos = Vector2.MoveTowards(body.position, target, speed * Time.fixedDeltaTime);
        body.MovePosition(newPos);

        if (Vector2.Distance(player.position, body.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}