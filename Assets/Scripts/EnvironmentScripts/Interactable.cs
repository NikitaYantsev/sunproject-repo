using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Interactable : MonoBehaviour
{
    Light2D bulb;
    public bool active = false;

    private void Awake()
    {
        bulb = gameObject.GetComponent<Light2D>();
    }

    // Object triggers whenether the player is within an interaction range (interaction collider)
    // While an object is active, we can display text or interaction button icon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            print("Can interact");
            active = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction")) 
        {
            print("Cannot interact");
            active = false;
        }
            
    }

    // Add here what happens if the player interacts with an object
    public void Interaction()
    {
        print("Interaction!");
    }
}
