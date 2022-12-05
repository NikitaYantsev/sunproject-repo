using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    // Grab all interactables from the scene, find the closest one, interact with it
    public void Interact()
    {
        Interactable interactable;
        GameObject interactableObject = FindClosestInteractible();
        if (interactableObject != null) 
        {
            interactable = interactableObject.GetComponent<Interactable>();
            if (interactable.active) {
                interactable.Interaction();
            }     
        }
    }

    public GameObject FindClosestInteractible()
    {
        GameObject[] interactibles;
        interactibles = GameObject.FindGameObjectsWithTag("Interactable");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject interactible in interactibles)
        {
            Vector3 diff = interactible.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = interactible;
                distance = curDistance;
            }
        }
        return closest;
    }
}
