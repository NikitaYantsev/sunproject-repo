using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditEnemyDetection : MonoBehaviour
{
    public Transform detectionPoint;
    public Vector2 detectionRange = new (4, 1);
    public LayerMask playerLayer;
    Collider2D character; 

    public bool DetectEnemies()
    {
        character = Physics2D.OverlapBox(detectionPoint.position, detectionRange, 0, playerLayer);

        return character;
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionPoint == null)
        {
            return;
        }
        Gizmos.DrawWireCube(detectionPoint.position, detectionRange);
    }
}