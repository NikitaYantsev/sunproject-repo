using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfStatements : MonoBehaviour
{
    float coffeeTemperature = 100.0f;
    float coffeeTooHot = 70.0f;
    float coffeeTooCold = 30.0f;
    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetTemperature();
        }

        coffeeTemperature -= Time.deltaTime * 5.0f;
    }

    void GetTemperature()
    {
        if (coffeeTemperature > coffeeTooHot)
        {
            print("Too hot!!");
        }
        else if (coffeeTemperature < coffeeTooCold)
        {
            print("Too cold!!!!");
        }
        else
        {
            print("Just fine");
        }
    }
}