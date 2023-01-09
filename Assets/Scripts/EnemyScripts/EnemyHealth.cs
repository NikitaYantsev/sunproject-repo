using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float balanceBound = 10;
    public float maxHealth = 100;
    public float damageResist = 0f;
    public bool parryAllowed = false;
    public float parryChance = 0f;
    float currentHealth;
    Rigidbody2D body;
    public Animator animator;
    DefenceMode defenceTrigger;
    DefenceMode allyDefenceTrigger;
    LayerMask enemyLayer;
    QTEScript qte;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defenceTrigger = GetComponent<DefenceMode>();
        currentHealth = maxHealth;
        body.freezeRotation = true;
        qte = GameObject.FindGameObjectWithTag("Player").GetComponent<QTEScript>();
        
    }

    //pass -1 if guaranteed stunlock is desired
    public void TakeDamage(float damage, float balanceDamage)
    {
        
        if (parryAllowed)
        {
            float ranNum = Random.Range(0f, 1f);
            if (ranNum < parryChance)
            {
                qte.StartQTE();
                return;
            }
        }
        
        currentHealth -= damage * (1f - damageResist);

        if (balanceDamage > balanceBound || balanceDamage == -1)
        {
            animator.SetTrigger("Hurt");
        }
        
        defenceTrigger.TriggerDefence(0);

        if (currentHealth <= 0)
        {
            Die();
        }   
    }

    void Die()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, 20f, enemyLayer);
        foreach (Collider2D enemy in nearbyEnemies) 
        { 
            if (enemy.TryGetComponent<DefenceMode>(out allyDefenceTrigger))
            {
                allyDefenceTrigger.TriggerDefence(1);
            }
        }

        print("He died");
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BanditEnemyDetection>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        //GetComponent<EnemyMovement>().enabled = false;
        body.gravityScale = 0;
        body.velocity = Vector3.zero;
        this.enabled = false;
    }
}