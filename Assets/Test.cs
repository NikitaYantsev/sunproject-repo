using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{   
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private float rightCoordinate;
    private float leftCoordinate;
    private Vector3 lastMousePos;
    private float timePassed;
    private bool win;
    private bool loose;

    // Start is called before the first frame update
    private void Start()
    {
        Color test = Color.blue;
        print(test);
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //_body = GetComponent<Transform>();
        var bounds = collider.bounds;
        //_leftCoordinate = bounds.min.x;
        rightCoordinate = bounds.max.x;
        leftCoordinate = bounds.min.x;
    }
    private void OnMouseOver()
    {
        if (win || loose) return;
        spriteRenderer.color = Color.yellow;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x >= rightCoordinate - (rightCoordinate - leftCoordinate) * 0.1)
        {
            Result(true);
            win = true;
        }
        timePassed += Time.deltaTime;
        print(timePassed);
        if (timePassed >= 1f)
            Result(false);
    }

    private void OnMouseExit()
    {
        if (win || loose) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x >= rightCoordinate)
        {
            Result(true);
            return;
        }
            Result(false);
    }

    private void Result(bool flag)
    {
        spriteRenderer.color = flag ? Color.green : Color.red;
        win = flag;
        loose = !flag;
    }
    // Update is called once per frame
    /*
    private void OnMouseDrag()
    {
        if (timePassed >= 0.2f && timePassed <= 2f)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            while (timePassed <= 2f && (lastMousePos - mousePos).x > 0)
            {
                if (mousePos.x >= rightCoordinate - (rightCoordinate - leftCoordinate) * 0.1)
                {
                    flag = true;
                }
            }
        
            spriteRenderer.color = flag ? Color.green : Color.red;
        }
        timePassed += Time.deltaTime;
    }
    */
}
