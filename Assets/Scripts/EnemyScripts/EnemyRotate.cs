using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    public Transform player;
    bool inBattle;
    bool isFlipped;

    private void Start()
    {
        //check if we need to rotate 
        if (transform.localScale.x > 0)
            isFlipped = false;
        else
            isFlipped = true;
    }
    public void LookAtPlayer()
    {

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        inBattle = true; // this function gets called only in battle mode

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }  
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!inBattle)
        {
            transform.localScale = new(-transform.localScale.x, transform.localScale.y);
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
