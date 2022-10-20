using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    float currentHealth;
    Rigidbody2D body; // need to be removed
    public Animator animator;

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // need to be removed
        animator = GetComponent<Animator>();    
        currentHealth = maxHealth;
        body.freezeRotation = true; // need to be removed

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        print("He died" );
        animator.SetBool("IsDead", true);
        
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BanditEnemyDetection>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        body.gravityScale = 0;
        this.enabled = false;
        
    }
}
