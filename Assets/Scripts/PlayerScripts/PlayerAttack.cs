using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    float attackTiming;
    public LayerMask enemyLayer;
    bool doSecondAttack;
    bool doThirdAttack;
    public float attackDamage = 40;
    public float lowBracket = 0.5f;
    public float highBracket = 0.6f;
    //KeyCode lastKeyCode;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame  
    void Update()
    {
        if (attackTiming > lowBracket)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //print("Second attack = " + doSecondAttack);
                //print("Third attack = "+ doThirdAttack);
            
                AttackController();
            }
        }
        attackTiming += Time.deltaTime;
    }   

    void AttackController()
    {      
        //third attack
        if (doThirdAttack && attackTiming > lowBracket && attackTiming < highBracket)
        {
            Attack(3);
            doThirdAttack = false;
            return;
        }
        else if (attackTiming < lowBracket || attackTiming > highBracket)
        {
            doThirdAttack = false;
            doSecondAttack = false;
            attackTiming = 0;
        }

        //second attack
        if (doSecondAttack && attackTiming > lowBracket && attackTiming < highBracket)
        {
            Attack(2);
            doThirdAttack = true;
            return;
        }
        else if (attackTiming < lowBracket || attackTiming > highBracket)
        {
            doSecondAttack = false;
            attackTiming = 0;
        }

        //first attack
        if (!doSecondAttack)
        { 
            Attack(1);
            doSecondAttack = true;
            return;
        }
    }
    
    void Attack(int attackType) 
    {
        switch (attackType)
        {
            case 1:
                animator.SetTrigger("Attack1");
                break;
            case 2:
                animator.SetTrigger("Attack2");
                break;
            case 3:
                animator.SetTrigger("Attack3");
                break;
        }
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
        attackTiming = 0;
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
