using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;


public class QTEScript : MonoBehaviour
{
    PlayerMovement mov_controls;
    PlayerUtilityControls ut_controls;
    PlayerAttack attack;
    Animator animator;
    Face weapon;
    Collider2D closestEnemy;
    public QTEButtons visuals;
    List<KeyCode> buttons;
    float timeLeft;

    KeyCode[] possibleButtons = { KeyCode.Q, KeyCode.A, KeyCode.Z, KeyCode.W, KeyCode.S, KeyCode.X, KeyCode.E, KeyCode.D, KeyCode.C,
                                   KeyCode.R, KeyCode.F, KeyCode.V, KeyCode.T, KeyCode.G, KeyCode.B};
    
    //Can add here another types if other QTE-modes needed
    enum Type : int
    {
        PlayerParry = 3
    }

    void Start()
    {
        mov_controls = GetComponent<PlayerMovement>();
        ut_controls = GetComponent<PlayerUtilityControls>();
        attack = GetComponent<PlayerAttack>();
        weapon = GetComponent<Face>();
        animator = GetComponent<Animator>();
        buttons = new List<KeyCode>();
    }

    //Blocks controls while keys for QTE exists
    private void Update()
    {
        if (buttons.Count > 0)
        {
            timeLeft -= Time.unscaledDeltaTime;
            if (timeLeft < 0)
            {
                Failure();
            }
            mov_controls.enabled = false;
            ut_controls.enabled = false;
            Time.timeScale = 0f;
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(buttons[0]))
                {
                    //3 is temporary variable, it could be changed if some other QTE will be used
                    int buttonID = Array.IndexOf(possibleButtons, buttons[0]);
                    visuals.PressButton(3 - buttons.Count, buttonID);
                    buttons.RemoveAt(0);
                    if (buttons.Count == 0) 
                    {
                        Success();
                    }
                }
                else
                {
                    Failure();
                }
            }
        }
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
            StartQTE();
            closestEnemy = FindClosestEnemy(enemiesThatCanBeParried);
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

    //Define which keys can be used and then generate three
    public void StartQTE(string purpose = "PlayerParry", float timeInSeconds = 3f)
    {
        if (buttons.Count == 0)
        {
            if (purpose == "PlayerParry") 
            {
                buttons = GetThreeButtons(possibleButtons, (int)Type.PlayerParry);
                int[] buttonsID = new int[3];
                for (int button = 0; button < 3; button++)
                {
                    int index = Array.IndexOf(possibleButtons, buttons[button]);
                    buttonsID[button] = index;
                }
                visuals.DrawLine(buttonsID);
                timeLeft = timeInSeconds;
            }          
        }        
    }

    //Generate three keys to pass in QTE
    List<KeyCode> GetThreeButtons(KeyCode[] possibleKeys, int count)
    {
        
        List<KeyCode> keysToPress = new List<KeyCode>();
        for (int i = 0; i < count; ++i)
        {
            int randomNum = UnityEngine.Random.Range(0, possibleKeys.Length - 1);
            keysToPress.Add(possibleKeys[randomNum]);
        }
        
        return keysToPress;
    }
    IEnumerator WaitAndReturnControls(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1;
        mov_controls.enabled = true;
        ut_controls.enabled = true;
    }

    void Success()
    {
        StartCoroutine(visuals.EraseButtons());
        Animator enemy_animator = closestEnemy.GetComponent<Animator>();
        StartCoroutine(WaitAndReturnControls(1f));
        animator.SetTrigger("SuccessfulParry");
        enemy_animator.SetTrigger("GotParried");
        weapon.AttackAfterSuccessfullParry();
    }

    void Failure()
    {
        buttons.Clear();
        print("Shit bro");
        StartCoroutine(visuals.EraseButtons());
        StartCoroutine(WaitAndReturnControls(1f));
        animator.SetTrigger("UnsuccessfulParry");
    }
}

