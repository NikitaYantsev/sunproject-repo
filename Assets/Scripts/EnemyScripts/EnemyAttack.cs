using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public float attackRate;
    public LayerMask playerLayer;
    public Animator animator;
    public float attackDamage;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (player && attackRate > 1f)
        {
            StartCoroutine(Attack(player));
        }
        else
        {
            attackRate += Time.deltaTime;
        }
    }
    IEnumerator Attack(Collider2D player) //DOESN'T WORK CORRECTLY WHILE SPAMMING LSHIFT, CAN ROLL WHILE ATTACKING
    {
        animator.SetTrigger("Attack");
        body.velocity = new Vector2(0, 0);
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        attackRate = 0;
        //GetComponent<EnemyMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        //GetComponent<EnemyMovement>().enabled = true;
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
