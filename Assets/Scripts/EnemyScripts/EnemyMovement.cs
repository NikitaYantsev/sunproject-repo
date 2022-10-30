using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Build;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyMovement : StateMachineBehaviour
{
    public float speed = -5f;
    public float attackRange = 3f;
    bool inBattle = false;
    public Transform player;
    Transform transform;
    BanditEnemyDetection detection;
    Rigidbody2D body;
    EnemyRotate rotate;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.GetComponent<Rigidbody2D>();
        rotate = animator.GetComponent<EnemyRotate>();
        detection = animator.GetComponent<BanditEnemyDetection>();
        transform = animator.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!inBattle)
        {
            Vector2 target = new(body.position.x + (-Mathf.Sign(transform.localScale.x) * 5f), body.position.y);
            Vector2 newPos = Vector2.MoveTowards(body.position, target, speed * Time.fixedDeltaTime);
            body.MovePosition(newPos);
            inBattle = detection.DetectEnemies();
        }
        else
        {
            rotate.LookAtPlayer();
            Vector2 target = new(player.position.x, body.position.y);
            Vector2 newPos = Vector2.MoveTowards(body.position, target, speed * 2 * Time.fixedDeltaTime);
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