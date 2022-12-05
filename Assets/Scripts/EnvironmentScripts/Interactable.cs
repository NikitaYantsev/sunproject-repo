using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Interactable : MonoBehaviour
{
    MeshRenderer text;
    Action action;
    public bool active = false;

    private void Awake()
    {
        text = GetComponentInChildren<MeshRenderer>();
        action = GetComponent<Action>();
    }

    // Object triggers whenether the player is within an interaction range (interaction collider)
    // While an object is active, we can display text or interaction button icon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            print("Can interact");
            active = true;
            text.enabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction")) 
        {
            print("Cannot interact");
            active = false;
            text.enabled = false;
        }
            
    }

    // Add here what happens if the player interacts with an object
    public void Interaction()
    {
        action.myAction();
        print("Interaction!");
    }
}
