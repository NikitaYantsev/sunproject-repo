using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    public Transform player;
    public bool inBattle;
    bool isFlipped = false;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

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
        inBattle = true; // this function gets triggered only in battle mode
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!inBattle)
        {
            print(transform.localScale.x);
            print(-Mathf.Sign(transform.localScale.x));
            transform.localScale = new(-transform.localScale.x, transform.localScale.y);
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
