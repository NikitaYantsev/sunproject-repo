using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    [SerializeField] float attackRange = 1;
    public LayerMask playerLayer;

    public float attackDamage;


    public void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (player != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
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
