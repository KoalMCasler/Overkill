using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    [SerializeField]
    private UIManager uIManager;
    [SerializeField]
    public GameObject player;
    public PlayerController playerController;
    [SerializeField]
    private LevelManager levelManager;
    public enum GameState{MainMenu, Gameplay}
    public GameState gameState;
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
        ChangeGameState();
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
    }

    void Gameplay()
    {
        player.SetActive(true);
        uIManager.SetUIGamePlay();
    }

    public void Save()
    {
        if(CheckforSave())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            Stats data = (Stats)bf.Deserialize(file);
            
            Stats playerSave = player.GetComponent<PlayerController>().playerStats;

            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

            Stats playerSave = player.GetComponent<PlayerController>().playerStats;


            bf.Serialize(file, playerSave);
            file.Close();   
        }
    }

    public bool CheckforSave()
    {
        bool doseSaveExisit = File.Exists(Application.persistentDataPath + "/playerInfo.dat");
        return doseSaveExisit;
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            Stats playerSave = (Stats)bf.Deserialize(file);
            file.Close();
            gameManager.player.GetComponent<PlayerController>().playerStats = playerSave;
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
            playerController.playerStats.baseDamage += .1f;
        }
        if(stat == "Speed")
        {
            playerController.playerStats.baseMoveSpeed += .5f;
        }
    }

    public void QuitGame()
    {
        //Debug line to test quit function in editor
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}