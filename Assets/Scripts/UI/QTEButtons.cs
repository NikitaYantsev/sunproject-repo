using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QTEButtons : MonoBehaviour
{
    public List<Sprite> keysToPress;
    public List<Sprite> pressedKeys;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    UnityEngine.UI.Image renderer1;
    UnityEngine.UI.Image renderer2;
    UnityEngine.UI.Image renderer3;

    UnityEngine.UI.Image[] renderers;

    private void Start()
    {
        renderer1 = button1.GetComponent<UnityEngine.UI.Image>();
        renderer2 = button2.GetComponent<UnityEngine.UI.Image>();
        renderer3 = button3.GetComponent<UnityEngine.UI.Image>();
        renderers = new UnityEngine.UI.Image[] { renderer1, renderer2, renderer3 };
    }

    public void DrawLine(int[] id)
    {
        for (int button = 0; button < 3; button++)
        {
            int index = id[button];
            Sprite myKey = keysToPress[index];
            renderers[button].overrideSprite = myKey;
            renderers[button].enabled = true;
        }

    }

    public void PressButton(int index, int id)
    {
        renderers[index].overrideSprite = pressedKeys[id];
    }

    public IEnumerator EraseButtons()
    {
        yield return new WaitForSeconds(0.1f);
        for (int button = 0; button < 3; button++)
        {
            renderers[button].enabled = false;
        }
    }


}
