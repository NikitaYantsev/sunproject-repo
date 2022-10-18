using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    int currentHealth;
    Rigidbody2D body; // need to be remove

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // need to be remove
        currentHealth = maxHealth;
        body.freezeRotation = true; // need to be remove

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        print("He died" );
    }
}
