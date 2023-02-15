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
        print(Vector2.Distance(player.position, body.position));
        if (Vector2.Distance(player.position, body.position) <= attackRange)
        {
            
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

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
