 using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;


public class ParryScript : MonoBehaviour
{
    TCharControls mov_controls;
    PlayerUtilityControls ut_controls;
    TCharAttack attack;
    Animator animator;
    Face weapon;
    Collider2D closestEnemy;
    public LineController visuals;
    private float timeLeft;
    private Line myLine;
    private int lineCount;

    //Can add here another types if other QTE-modes needed
    enum Type : int
    {
        PlayerParry = 3
    }

    void Start()
    {
        mov_controls = GetComponent<TCharControls>();
        ut_controls = GetComponent<PlayerUtilityControls>();
        attack = GetComponent<TCharAttack>();
        weapon = GetComponent<Face>();
        animator = GetComponent<Animator>();
    }

    //Blocks controls while keys for QTE exists
    private void Update()
    {
        if (lineCount <= 0) return;
        timeLeft -= Time.unscaledDeltaTime;
        if (timeLeft < 0)
        {
            //Failure();
        }
        mov_controls.enabled = false;
        //ut_controls.enabled = false;
        Time.timeScale = 0f;
    }

    public void CheckAndParryOne()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack.attackPoint.position, weapon.AttackRange, attack.enemyLayer);
        List<Collider2D> enemiesThatCanBeParried = new List<Collider2D>();
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyState enemyStats = enemy.GetComponent<EnemyState>();
            if (enemyStats.canBeParried)
                enemiesThatCanBeParried.Add(enemy);
        }
        if (enemiesThatCanBeParried.Count > 0)
        {
            closestEnemy = FindClosestEnemy(enemiesThatCanBeParried);
            StartParry(closestEnemy.transform);
        }
        else
        {
            print("LOSER");
            animator.SetTrigger("UnsuccessfulParry");
        }
    }

    Collider2D FindClosestEnemy(List<Collider2D> enemiesArray)
    {
        Collider2D closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Collider2D enemy in enemiesArray)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }
        return closest;
    }
    
    public void StartParry(Transform parentPosition, string purpose = "PlayerParry", float timeInSeconds = 3f)
    {
        if (purpose == "PlayerParry")
        {
            myLine = visuals.InstantiateLine(parentPosition);
            lineCount += 1;
            timeLeft = timeInSeconds;
        }
    }
    
    IEnumerator WaitAndReturnControls(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1;
        mov_controls.enabled = true;
        //ut_controls.enabled = true;
    }

    public void Success()
    {
        print("Success!");
        print(myLine);
        lineCount--;
        Destroy(myLine.gameObject, 0.2f);
        Animator enemyAnimator = closestEnemy.GetComponent<Animator>();
        StartCoroutine(WaitAndReturnControls(1f));
        animator.SetTrigger("SuccessfulParry");
        enemyAnimator.SetTrigger("GotParried");
        weapon.AttackAfterSuccessfullParry();
    }

    public void Failure()
    {
        print("Shit!");
        lineCount--;
        Destroy(myLine.gameObject, 0.2f);
        StartCoroutine(WaitAndReturnControls(1f));
        animator.SetTrigger("UnsuccessfulParry");
    }
}

