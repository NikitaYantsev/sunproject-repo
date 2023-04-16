using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLine : Line
{
    private void OnMouseEnter()
    {
        //print(_leftCoordinate + "on Mouse Enter");
        float length = RightCoordinate - LeftCoordinate;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MinimumToPassFromLeft = LeftCoordinate + (length) * 0.2;
        MinimumToPassFromRight = RightCoordinate - (length) * 0.2;
        if (mousePos.x >= MinimumToPassFromLeft && mousePos.x <= MinimumToPassFromRight)
            Result(false);
        StartedFromLeft = Math.Abs(mousePos.x - LeftCoordinate) < Math.Abs(mousePos.x - RightCoordinate);
    }

    private void OnMouseOver()
    {
        if (win || loose) return;
        spriteRenderer.color = Color.yellow;
        TimePassed += Time.deltaTime;
        if (TimePassed >= 1f)
            Result(false);
    }

    private void OnMouseExit()
    {
        print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (win || loose) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        switch (StartedFromLeft)
        {
            case true when mousePos.x >= MinimumToPassFromRight:
                Result(true); 
                break;
            case false when mousePos.x <= MinimumToPassFromLeft:
                Result(true); 
                break;
            default:
                Result(false);
                break;
        }
    }
}