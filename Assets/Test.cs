using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Collider2D _collider;
    private Transform _body;
    private float _leftCoordinate;
    private float _rightCoordinate;
    private int[] _percentageArray;
    private float step;
    
    // Start is called before the first frame update
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Transform>();
        var bounds = _collider.bounds;
        _leftCoordinate = bounds.min.x;
        _rightCoordinate = bounds.max.x;
        step = Math.Abs(_rightCoordinate - _leftCoordinate) / 100;
        _percentageArray = new int[100];
    }

    // Update is called once per frame
    private void OnMouseDrag()
    {
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int currentPercent = (int)((mousePos.x - _leftCoordinate) / step);
            print($"{mousePos.x} - {_leftCoordinate} / {step}");
            print(currentPercent);
            if (currentPercent < 99)
            {
                _percentageArray[currentPercent] = 1;
            }

        }
    }

    private void OnMouseExit()
    {
        foreach (var i in _percentageArray)
        {
            print(string.Join(' ', _percentageArray));
        }
    }
}
