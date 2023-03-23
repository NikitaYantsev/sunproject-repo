using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParryTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform init_pos = GetComponentInParent<Transform>();
        print(init_pos.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
