using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Transform interactionPoint;
    [SerializeField] float interactionRange;
    LayerMask interactiveLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D interactiveObject = Physics2D.OverlapCircle(interactionPoint.position, interactionRange, interactiveLayer);
        if (interactiveObject)
        {
           // interactiveObject.GetComponent<Interactable>().setActive();
        }

    }
}