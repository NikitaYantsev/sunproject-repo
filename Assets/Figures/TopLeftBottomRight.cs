using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeftBottomRight : Figure
{
    private Vector3[] coordinates = { new Vector3(0f, 0f, 0f),
                                      new Vector3(0f, 0f, 0f) };
    
    private Quaternion[] rotations = { Quaternion.Euler(0, 0, 45),
                                       Quaternion.Euler(0, 0, -45) };

    private GameObject[] lines = { MyHorizontalLine, MyVerticalLine };
    
    public override Vector3[] GetCoordinates
    {
        get => coordinates;
    }

    public override Quaternion[] GetRotations
    {
        get => rotations;
    }

    public override GameObject[] GetLines
    {
        get => lines;
    }

}
