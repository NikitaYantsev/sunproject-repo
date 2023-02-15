using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 3;
    public float currentHealth;
    //Change it through parry animation later!!
    public bool invulnerable = false;
    public Animator animator;
    public Rigidbody2D body;
    [SerializeField] float balanceBound;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, float balanceDamage)
    {
        if (!invulnerable)
        {
            currentHealth -= damage;
            if (balanceDamage > balanceBound)
            {
                animator.SetTrigger("Hurt");
            }
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        print("You dead");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        animator.SetBool("isRunning", false);
        animator.SetBool("IsDead", true);
        body.gravityScale = 0;
        body.velocity = Vector3.zero;
        this.enabled = false;
    }
}

