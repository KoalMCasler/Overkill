using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().health -= GameManager.gameManager.playerController.playerStats.damage;
        }
        Destroy(this.gameObject);
    }
}
