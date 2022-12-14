using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;


public class QTEScript : MonoBehaviour
{
    PlayerMovement controls;
    public QTEButtons visuals;
    List<KeyCode> buttons;
    float timeLeft;

    Face currentWeapon;    // !!Change here to test new weapon!!

    KeyCode[] possibleButtons = { KeyCode.Q, KeyCode.A, KeyCode.Z, KeyCode.W, KeyCode.S, KeyCode.X, KeyCode.E, KeyCode.D, KeyCode.C,
                                   KeyCode.R, KeyCode.F, KeyCode.V, KeyCode.T, KeyCode.G, KeyCode.B};
    
    //Can add here another types if other QTE-modes needed
    enum Type : int
    {
        PlayerParry = 3
    }

    void Start()
    {
        controls = GetComponent<PlayerMovement>();
        buttons = new List<KeyCode>();

        currentWeapon = GetComponent<Face>(); // !!Change here to test new weapon!!
    }

    //Blocks controls while keys for QTE exists
    private void Update()
    {
        if (buttons.Count > 0)
        {
            timeLeft -= Time.unscaledDeltaTime;
            if (timeLeft < 0)
            {
                Failure();
            }
            controls.enabled = false;
            Time.timeScale = 0f;
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(buttons[0]))
                {
                    //3 is temporary variable, it could be changed if some other QTE will be used
                    int buttonID = Array.IndexOf(possibleButtons, buttons[0]);
                    visuals.PressButton(3 - buttons.Count, buttonID);
                    buttons.RemoveAt(0);
                    if (buttons.Count == 0) 
                    {
                        Success();
                    }
                }
                else
                {
                    Failure();
                }
            }
        }
    }

    //Define which keys can be used and then generate three
    public void StartQTE(string purpose = "PlayerParry", float timeInSeconds = 2f)
    {
        if (buttons.Count == 0)
        {
            if (purpose == "PlayerParry") 
            {
                buttons = GetThreeButtons(possibleButtons, (int)Type.PlayerParry);
                int[] buttonsID = new int[3];
                for (int button = 0; button < 3; button++)
                {
                    int index = Array.IndexOf(possibleButtons, buttons[button]);
                    buttonsID[button] = index;
                }
                visuals.DrawButtons(buttonsID);
                timeLeft = timeInSeconds;
            }          
        }        
    }

    //Generate three keys to pass in QTE
    List<KeyCode> GetThreeButtons(KeyCode[] possibleKeys, int count)
    {
        
        List<KeyCode> keysToPress = new List<KeyCode>();
        for (int i = 0; i < count; ++i)
        {
            int randomNum = UnityEngine.Random.Range(0, possibleKeys.Length - 1);
            keysToPress.Add(possibleKeys[randomNum]);
        }
        
        return keysToPress;
    }
    IEnumerator WaitAndReturnControls(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1;
        controls.enabled = true; 
    }

    void Success()
    {
        StartCoroutine(visuals.EraseButtons());
        StartCoroutine(WaitAndReturnControls(0.5f));
        currentWeapon.AttackAfterSuccessfullParry();
    }

    void Failure()
    {
        buttons.Clear();
        print("Shit bro");
        StartCoroutine(visuals.EraseButtons());
        StartCoroutine(WaitAndReturnControls(0.5f));
    }
}

