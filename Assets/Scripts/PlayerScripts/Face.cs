using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Face : MonoBehaviour
{ 
    PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    public float Damage = 30;
    public float AttackRange = 10;
    public float BalanceDamage = 20;

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
        print("Special attack");
    }

    public void AttackAfterSuccessfullParry()
    {
        print("AttackAfterSuccessfullParry");
    }
}
