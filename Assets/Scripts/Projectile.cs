using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<EnemyAI>().health -= gameManager.player.playerStats.damage;
            Destroy(this);
        }
    }
}
