using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 10;
    public float currentStamina;
    Animator animator;

    private void Start()
    {
        currentStamina = maxStamina;
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (currentStamina < maxStamina)
            currentStamina += Time.deltaTime;

        if (animator.GetBool("isRolling") && currentStamina > 0)
        {
            currentStamina -= Time.deltaTime * 6;
        }

        if (animator.GetBool("Sprint") && currentStamina > 0)
        {
            currentStamina -= Time.deltaTime * 3;
        }
    }
}
