using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject boom;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Projectile hit " + other.gameObject.name);
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().health -= GameManager.gameManager.playerController.playerStats.damage;
            other.gameObject.GetComponent<EnemyAI>().enemyAnim.SetTrigger("IsHit");
        }
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        GameObject particles = Instantiate(boom, this.transform.position, this.transform.rotation);
        Destroy(particles,.5f);
    }
}
