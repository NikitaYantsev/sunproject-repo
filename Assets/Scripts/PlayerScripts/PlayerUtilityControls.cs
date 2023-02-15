using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUtilityControls : MonoBehaviour
{

    PlayerInteraction interactionScript;
    Animator animator;
    Rigidbody2D body;
    QTEScript parryScript;
    PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        interactionScript = GetComponent<PlayerInteraction>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        parryScript = GetComponent<QTEScript>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactionScript.Interact();
        }

        //Parry
        if (Input.GetKeyDown(KeyCode.F) && animator.GetBool("isGrounded"))
        {
            body.velocity = new Vector2(0, 0);
            movement.horizontalInput = 0;
            animator.SetBool("Sprint", false);
            parryScript.CheckAndParryOne();
        }
    }
}

    
