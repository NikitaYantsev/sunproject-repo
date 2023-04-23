using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParryTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform init_pos = GetComponentInParent<Transform>();
        var MyVerticalLine = GameObject.FindGameObjectWithTag("VerticalLine");
        var MyHorizontalLine = GameObject.FindGameObjectWithTag("HorizontalLine");
        Figure.SetHorizontal(MyHorizontalLine);
        Figure.SetVertical(MyVerticalLine);
        var my_figure = Figure.GetFigure("TopLeftBottomRight");
        InstantiatePattern.Instantiate(my_figure);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
