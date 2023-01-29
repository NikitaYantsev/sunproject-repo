using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Face : MonoBehaviour
{ 
    PlayerAttack playerAttack;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public float Damage = 30;
    public float AttackRange = 1;
    public float BalanceDamage = 20;
    float intervalFromLastSA;

    private void Update()
    {
        if (intervalFromLastSA < 1f)
            intervalFromLastSA += Time.deltaTime;
    }

    public void FinalAttackInCombo(float damage, float attackRange)
    {
        IEnumerator func = FinalAttack(damage, attackRange);
        StartCoroutine(func);
    }

    IEnumerator FinalAttack(float damage, float attackRange)
    {
        for (int i = 0; i < 10; i++)
        {
            playerAttack.Attack(3, damage, attackRange, -1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SpecialAttack()
    {
        if (intervalFromLastSA > 0.5f)
        {
            intervalFromLastSA = 0f;
            print("Special Attack!!");
            Collider2D[] enemiesInRange = playerAttack.GetEnemies(AttackRange);
            foreach (Collider2D enemy in enemiesInRange)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(Damage * 0.01f, BalanceDamage);
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f * transform.localScale.x, 0f), ForceMode2D.Impulse);
            }
        }

    }

    public void SlowDownPlayer()
    {
        playerMovement.moveSpeed = 3.5f;
    }

    public void ResetPlayer()
    {
        playerMovement.moveSpeed = 7f;
    }

    public void AttackAfterSuccessfullParry()
    {
        print("AttackAfterSuccessfullParry");
    }
}
