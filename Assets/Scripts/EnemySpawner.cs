using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints = new Transform[8];
    public GameObject[] enemies = new GameObject[9];
    public GameManager gameManager;
    public float spawnTimer;
    public float spanwDelay;
    public int spanwRoll;
    // Start is called before the first frame update
    void Start()
    {
        if(spanwDelay != 5)
        {
            spanwDelay = 5;
        }
        spanwRoll = 0;
        gameManager = FindObjectOfType<GameManager>();
        for(int i = 0; i < 8; i++)
        {
            SpawnRandomEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spanwDelay && GameManager.gameManager.playerController.playerStats.isAlive)
        {
            SpawnRandomEnemy();
            spawnTimer = 0;
        }
        if(gameManager.runTime > 60 && gameManager.runTime < 120)
        {
            spanwDelay = 3;
        }
        else if(gameManager.runTime > 120)
        {
            spanwDelay = 1.5f;
        }
    }

    void SpawnRandomEnemy()
    {
        int enemyRoll = 0;
        if(gameManager.runTime < 60)
        {
            enemyRoll = Random.Range(0,3);
        }
        else if(gameManager.runTime < 120 && gameManager.runTime > 60)
        {
            enemyRoll = Random.Range(3, 6);
        }
        else if(gameManager.runTime < 180)
        {
            enemyRoll = Random.Range(7,9);
        }
        //Debug.Log(enemyRoll);
        GameObject enemiy = Instantiate(enemies[enemyRoll], spawnPoints[spanwRoll].position, spawnPoints[spanwRoll].rotation);
        spanwRoll += 1;
        if(spanwRoll >= 8)
        {
            spanwRoll = 0;
        }
    }

}
