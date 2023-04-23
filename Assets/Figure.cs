using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;

public class Figure
{
    private GameObject[] lines;
    private Vector3[] coordinates;
    private Quaternion[] rotations;

    protected static GameObject MyVerticalLine { get; set; }
    protected static GameObject MyHorizontalLine { get; set; }

    public static void SetVertical(GameObject value)
    {
        MyVerticalLine = value;
    }

    public static void SetHorizontal(GameObject value)
    {
        MyHorizontalLine = value;
    }

    public virtual Vector3[] GetCoordinates
    {
        get => coordinates;
    }

    public virtual Quaternion[] GetRotations
    {
        get => rotations;
    }

    public virtual GameObject[] GetLines
    {
        get => lines;
    }
    
    public static Figure GetFigure(string type)
    {
        if (type == "TopLeftBottomRight")
        {
            return new TopLeftBottomRight();
        }

        return null;
    }
}
