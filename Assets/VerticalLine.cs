using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLine : Line
{
    private void OnMouseEnter()
    {
        //print(_leftCoordinate + "on Mouse Enter");
        float height = TopCoordinate - BottomCoordinate;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MinimumToPassFromTop = TopCoordinate - (height) * 0.2;
        MinimumToPassFromBottom = BottomCoordinate + (height) * 0.2;
        if (mousePos.y <= MinimumToPassFromTop && mousePos.y >= MinimumToPassFromBottom)
            Result(false);
        StartedFromBottom = Math.Abs(mousePos.y - BottomCoordinate) < Math.Abs(mousePos.y - TopCoordinate);
    }

    private void OnMouseOver()
    {

        if (win || loose) return;
        spriteRenderer.color = Color.yellow;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        TimePassed += Time.deltaTime;
        if (TimePassed >= 1f)
            Result(false);
    }

    private void OnMouseExit()
    {
        print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (win || loose) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch (StartedFromBottom)
        {
            case true when mousePos.y >= MinimumToPassFromTop:
                Result(true); 
                break;
            case false when mousePos.y <= MinimumToPassFromBottom:
                Result(true); 
                break;
            default:
                Result(false);
                break;
        }
    }
}
