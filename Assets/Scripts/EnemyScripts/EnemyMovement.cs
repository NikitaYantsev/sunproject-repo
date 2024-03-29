using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyMovement : StateMachineBehaviour
{
    public float speed = -5f;
    public bool inBattle = false;
    public Transform player;
    Transform transform;
    BanditEnemyDetection detection;
    Rigidbody2D body;
    EnemyRotate rotate;
    DefenceMode defenceTrigger;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.GetComponent<Rigidbody2D>();
        rotate = animator.GetComponent<EnemyRotate>();
        detection = animator.GetComponent<BanditEnemyDetection>();
        transform = animator.GetComponent<Transform>();
        defenceTrigger = animator.GetComponent<DefenceMode>();
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
            defenceTrigger.TriggerDefence(2);
            animator.SetBool("inBattle", inBattle);
            rotate.LookAtPlayer();
            Vector2 target = new(player.position.x, body.position.y);
            Vector2 newPos = Vector2.MoveTowards(body.position, target, speed * 2 * Time.fixedDeltaTime);
            body.MovePosition(newPos);           
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}