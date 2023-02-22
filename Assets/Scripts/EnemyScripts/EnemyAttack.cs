using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public Transform player;
    Animator animator;
    Rigidbody2D body;
    bool isAttacking = false;
    [SerializeField] public float attackRange;
    [SerializeField] float balanceDamage;
    public LayerMask playerLayer;
    [SerializeField] float attackDamage;
    EnemyState stats;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyState>();
    }

    private void Update()
    {

        if (Vector2.Distance(player.position, body.position) <= attackRange && !isAttacking)
        {
            print(Vector2.Distance(player.position, body.position) + " <= " + attackRange);
            print("Attack");
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (player != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage, balanceDamage);
        }
    }

    public void CanParry()
    {
        stats.canBeParried = true;
    }

    public void CannotParry()
    {
        stats.canBeParried = false;
    }

    public void CanAttack() 
    {
        isAttacking = false;
    }

    public void CannotAttack()
    {
        isAttacking = true;
    }



    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
