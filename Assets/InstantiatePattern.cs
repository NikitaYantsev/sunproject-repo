using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantiatePattern : MonoBehaviour
{
    static public Transform parentObject;
    
    public static void Instantiate(Figure figure) //, Transform parentObject)
    {
        Vector3[] coordinates = figure.GetCoordinates;
        Quaternion[] rotations = figure.GetRotations;
        GameObject[] lines = figure.GetLines;
        for (int i = 0; i < coordinates.Length; i++)
        {
            Instantiate(lines[i], coordinates[i], rotations[i], parentObject);
        }
    }
        
}
