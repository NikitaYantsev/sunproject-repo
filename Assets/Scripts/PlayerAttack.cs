using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    [SerializeField] int attackDamage = 40;
    float attackCooldown;

    // Update is called once per frame
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attackCooldown += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            if (attackCooldown > 1)
                Attack();
                
    }   

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
        attackCooldown = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
