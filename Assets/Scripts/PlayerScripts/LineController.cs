using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private Line line;

        // Start is called before the first frame update
    public Line InstantiateLine(Transform parentObject)
    {
        // make it to return Line type instead of game object
        Vector3 pos = parentObject.position;
        pos = new Vector3(pos.x, pos.y, pos.z - 1);
        var myLine = Instantiate(line, pos, Quaternion.identity);
        return myLine;
    }
}
