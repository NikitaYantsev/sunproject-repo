using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceMode : MonoBehaviour
{
    [SerializeField] float damageReducePercent;
    [SerializeField] float parryChance;

    EnemyHealth health;

    public enum Type 
    {
        ReduceDamage,
        AllowParry,
        SpecialAttack
    }

    public Type type;

    public enum Trigger
    {
        TakeDamage,
        AllyDies,
        PlayerDetected
    }

    public Trigger trigger;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    public void TriggerDefence(int detectedTrigger)
    {
        if (detectedTrigger == (int)trigger)
        {
            Defence();
        }
    }

    public void Defence()
    {
        if (type == Type.ReduceDamage)
        {
            health.damageResist += damageReducePercent;
        }

        if (type == Type.AllowParry)
        {
            health.parryAllowed = true;
            health.parryChance = this.parryChance;
        }

        if (type == Type.SpecialAttack)
        {
            print("Do something cool!");
        }
    }
}
