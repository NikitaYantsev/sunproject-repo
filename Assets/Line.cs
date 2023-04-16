using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Line : MonoBehaviour
{
    private Collider2D collider;
    protected SpriteRenderer spriteRenderer;
    
    private float rightCoordinate;    
    private float leftCoordinate;
    
    protected double MinimumToPassFromLeft;
    protected double MinimumToPassFromRight;
    protected double MinimumToPassFromTop;
    protected double MinimumToPassFromBottom;
    
    protected float TopCoordinate;
    protected float BottomCoordinate;
    
    protected bool StartedFromBottom;
    protected bool StartedFromLeft;
    
    public float RightCoordinate
    {
        get => rightCoordinate;

        set
        {
            if (rightCoordinate == 0)
                rightCoordinate = value;
        }
    }

    
    public float LeftCoordinate
    {
        get => leftCoordinate;

        set
        {
            if (leftCoordinate == 0)
                leftCoordinate = value;
        }
    }
    protected float TimePassed;
    protected bool win;
    protected bool loose;
    private ParryScript parryScript;


    // Start is called before the first frame update
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        var bounds = collider.bounds;
        rightCoordinate = bounds.max.x;
        leftCoordinate = bounds.min.x;
        TopCoordinate = bounds.max.y;
        BottomCoordinate = bounds.min.y;
        //parryScript = GameObject.FindWithTag("Player").GetComponent<ParryScript>();
    }
    
    protected void Result(bool flag)
    {
        spriteRenderer.color = flag ? Color.green : Color.red;
        win = flag;
        loose = !flag;
        
        /*
        if (win)
            parryScript.Success();
        if (loose)
            parryScript.Failure();
        */
        Destroy(this.gameObject, 0.2f);
    }
}
