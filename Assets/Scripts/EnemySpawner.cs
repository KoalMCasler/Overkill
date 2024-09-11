using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints = new Transform[8];
    public GameObject[] enemies = new GameObject[9];
    public GameManager gameManager;
    public float spawnTimer;
    public int spanwDelay;
    public int spanwRoll;
    // Start is called before the first frame update
    void Start()
    {
        spanwRoll = 0;
        if(spanwDelay < 5)
        {
            spanwDelay = 5;
        }
        gameManager = FindObjectOfType<GameManager>();
        SpawnRandomEnemy();
        SpawnRandomEnemy();
        SpawnRandomEnemy();
        SpawnRandomEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spanwDelay)
        {
            SpawnRandomEnemy();
            spawnTimer = 0;
        }
    }

    void SpawnRandomEnemy()
    {
        int enemyRoll = 0;
        if(gameManager.runTime < 60)
        {
            enemyRoll = Random.Range(0,2);
        }
        else if(gameManager.runTime < 120 && gameManager.runTime > 60)
        {
            enemyRoll = Random.Range(3,5);
        }
        else if(gameManager.runTime < 180)
        {
            enemyRoll = Random.Range(7,9);
        }
        GameObject enemiy = Instantiate(enemies[enemyRoll], spawnPoints[spanwRoll].position, spawnPoints[spanwRoll].rotation);
        spanwRoll += 1;
        if(spanwRoll > 8)
        {
            spanwRoll = 0;
        }
    }

}
