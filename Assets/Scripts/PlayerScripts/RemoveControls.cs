using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveControls : MonoBehaviour
{

    PlayerMovement controls;
    PlayerUtilityControls utControls;

    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<PlayerMovement>();
        utControls = GetComponent<PlayerUtilityControls>();
    }

    public void EnableControls()
    {
        controls.enabled = true;
        utControls.enabled = true;
    }

    public void DisableControls()
    {
        controls.enabled = false;
        utControls.enabled = false;
    }      
}
