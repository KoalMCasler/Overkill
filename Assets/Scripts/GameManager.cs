using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    [SerializeField]
    public UIManager uIManager;
    [SerializeField]
    public SoundManager soundManager;
    [SerializeField]
    public GameObject player;
    public PlayerController playerController;
    [SerializeField]
    private LevelManager levelManager;
    public enum GameState{MainMenu, Gameplay}
    public GameState gameState;
    private Stats loadedStats;
    [Header("Run Statistics")]
    public int killCount;
    public float runTime;
    public float roundBonus;
    public float totalEarned;
    void Awake()
    {
        if(gameManager != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            gameManager = this;
        }
        playerController = player.GetComponent<PlayerController>();
        loadedStats = ScriptableObject.CreateInstance<Stats>();
        ChangeGameState();
    }

    void Update()
    {
        if(gameState == GameState.Gameplay && playerController.playerStats.isAlive)
        {
            runTime += Time.deltaTime;
        }
        playerController.playerStats.killCount = killCount;
    }

    public void ChangeGameState()
    {
        switch(gameState)
        {
            case GameState.MainMenu: MainMenu(); break;
            case GameState.Gameplay: Gameplay(); break;
        }
    }

    void MainMenu()
    {
        player.SetActive(false);
        uIManager.SetUIMainMenu();
        soundManager.music.clip = soundManager.mainMusic;
        soundManager.music.Play();
    }

    void Gameplay()
    {
        player.SetActive(true);
        playerController.playerStats.isAlive = true;
        runTime = 0;
        killCount = 0;
        playerController.playerStats.killCount = 0;
        uIManager.SetUIGamePlay();
        soundManager.music.clip = soundManager.gameMusic;
        soundManager.music.Play();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.json");

        Stats playerSave = player.GetComponent<PlayerController>().playerStats;
        string json = JsonUtility.ToJson(playerSave);

        bf.Serialize(file, json);
        file.Close();   
    }

    public bool CheckforSave()
    {
        bool doseSaveExisit = File.Exists(Application.persistentDataPath + "/playerInfo.json");
        return doseSaveExisit;
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.json"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.json", FileMode.Open);

            string json = (string)bf.Deserialize(file);
            
            file.Close();
            JsonUtility.FromJsonOverwrite(json, loadedStats);
            gameManager.player.GetComponent<PlayerController>().playerStats = loadedStats;

        }
    }

    public void IncreaseStat(string stat)
    {
        if(stat == "Armor")
        {
            playerController.playerStats.maxHP += 25;
        }
        if(stat == "Damage")
        {
            playerController.playerStats.baseDamage += .25f;
        }
        if(stat == "Speed")
        {
            playerController.playerStats.baseMoveSpeed += .5f;
        }
        playerController.playerStats.upgradePoints -= 100;
    }

    public void QuitGame()
    {
        //Debug line to test quit function in editor
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void CalculateResults()
    {
        if(playerController.playerStats.currentHP == 0)
        {
            roundBonus = 1f;
        }
        else if(playerController.playerStats.currentHP < 0 && playerController.playerStats.currentHP > -5)
        {
            roundBonus = 1.5f;
        }
        else if(playerController.playerStats.currentHP < -5 && playerController.playerStats.currentHP > -15)
        {
            roundBonus = 1.75f;
        }
        else if(playerController.playerStats.currentHP < -15)
        {
            roundBonus = 2f;
        }
        totalEarned = ((killCount * 5) + runTime) * roundBonus;
        playerController.playerStats.upgradePoints += totalEarned;
    }
}