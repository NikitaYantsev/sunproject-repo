using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Interactable : MonoBehaviour
{
    Light2D bulb;
    bool canInteract = false;

    private void Awake()
    {
        bulb = gameObject.GetComponent<Light2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            bulb.enabled = true;
            canInteract = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            bulb.enabled = !bulb.enabled;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bulb.enabled = false;
    }
}
