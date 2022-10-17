using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Renderer triangleRenderer;
    bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        //triangleRenderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (flag == false)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
        }
        if (flag == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                transform.Rotate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }
    }
    private void OnMouseDown()
    {
        print("I\'m the triangle!");
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
