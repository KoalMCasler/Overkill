using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int moveSpeed;
    public int damage;
    public Rigidbody2D rb;
    public Vector2 directionToTarget;
    public Quaternion targetRotation;
    public Quaternion rotation;
    public float rotationSpeed;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.gameManager.player;
        health = maxHealth;
        rotationSpeed = 5;
        rb = this.GetComponent<Rigidbody2D>();
        RotateTowardsTarget();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();
        Move();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().playerStats.currentHP -= damage;
            rb.AddForce(-transform.up * 1000, ForceMode2D.Impulse);
        }
    }

    void RotateTowardsTarget()
    {
        Debug.Log("Rotating to target");
        Vector2 targetDirecion = player.transform.position - transform.position;
        directionToTarget = targetDirecion.normalized;

        targetRotation = Quaternion.LookRotation(transform.forward, directionToTarget);
        rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        rb.SetRotation(rotation);
    }

    void Move()
    {
        RotateTowardsTarget();
        rb.velocity = transform.up * moveSpeed;
    }


}
