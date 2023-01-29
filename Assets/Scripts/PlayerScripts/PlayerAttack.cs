using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;

    Face currentWeapon;    // !!Change here to test new weapon!!

    float attackTiming;
    public LayerMask enemyLayer;
    bool doSecondAttack;
    bool doThirdAttack;
    public float lowBracket = 0.5f;
    public float highBracket = 0.6f;
    //KeyCode lastKeyCode;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //Consider making PlayerStats class and take damage and other values from there
        currentWeapon = GetComponent<Face>(); // !!Change here to test new weapon!!
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            if (attackTiming > lowBracket)
            {
                AttackController();
            }
            
        }
        attackTiming += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Mouse1))
            currentWeapon.SlowDownPlayer();

        if (Input.GetKey(KeyCode.Mouse1))
        {
            print("Key pressed");
            currentWeapon.SpecialAttack();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            print("Key released");
            currentWeapon.ResetPlayer(); 
        }
          


        
    }   

    void AttackController()
    {      
        //third attack
        if (doThirdAttack && attackTiming > lowBracket && attackTiming < highBracket)
        {
            currentWeapon.FinalAttackInCombo(currentWeapon.Damage, currentWeapon.AttackRange);
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
            Attack(2, currentWeapon.Damage, currentWeapon.AttackRange, currentWeapon.BalanceDamage);
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
            Attack(1, currentWeapon.Damage, currentWeapon.AttackRange, currentWeapon.BalanceDamage);
            doSecondAttack = true;
            return;
        }
    }
    
    public void Attack(int attackType, float attackDamage, float attackRange, float balanceDamage) 
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
        print(attackRange);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage, balanceDamage);
        }
        attackTiming = 0;
    }

    public Collider2D[] GetEnemies(float attackRange)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        return hitEnemies;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, 1);
    }
}
