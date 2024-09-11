using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Referances")]
    public GameObject mainMenu;
    public GameObject startMenu;
    public GameObject upgradeMenu;
    public GameObject runEndMenu;
    public GameObject creditsMenu;
    public Button loadButton;
    [Header("Upgrades Menu")]
    public TextMeshProUGUI statsDisplay;
    public Button armorUpButton;
    public Button damageUpButton;
    public Button speedUpButton;
    public TextMeshProUGUI pointsDisplay;
    [Header("HUD")]
    public GameObject hUDObject;
    public TextMeshProUGUI healthUI;
    public Slider healthSlider;
    public TextMeshProUGUI killCountHUD;
    public TextMeshProUGUI runTimeHUD;
    public GameManager gameManager;
    [Header("Run Results")]
    public TextMeshProUGUI runTimeText;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI bonusText;
    public TextMeshProUGUI totalText;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(hUDObject.activeSelf)
        {
            healthSlider.value = gameManager.player.GetComponent<PlayerController>().playerStats.currentHP;
            healthUI.text = string.Format("{0}/{1}",gameManager.player.GetComponent<PlayerController>().playerStats.currentHP,gameManager.player.GetComponent<PlayerController>().playerStats.maxHP);
            healthSlider.minValue = 0;
            healthSlider.maxValue = gameManager.player.GetComponent<PlayerController>().playerStats.maxHP;
            killCountHUD.text = string.Format("Kills: {0}", gameManager.killCount);
            var timespan = TimeSpan.FromSeconds(gameManager.runTime);
            runTimeHUD.text = string.Format("Run Time: {0}", timespan.ToString(@"mm\:ss"));
        }
        if(runEndMenu.activeSelf)
        {
            UpdateResults();
        }
        if(mainMenu.activeSelf)
        {
            loadButton.interactable = gameManager.CheckforSave();
        }
        if(upgradeMenu.activeSelf)
        {
            CanYouAfordTheUpgrade();
            UpdateStats();
            pointsDisplay.text = string.Format("Points = {0:.}", gameManager.playerController.playerStats.upgradePoints);
        }
    }

    public void SetUIMainMenu()
    {
        hUDObject.SetActive(false);
        startMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        runEndMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        gameManager.soundManager.music.clip = gameManager.soundManager.mainMusic;
        gameManager.soundManager.music.Play();
    }

    public void SetUIStartMenu()
    {
        creditsMenu.SetActive(false);
        hUDObject.SetActive(false);
        upgradeMenu.SetActive(false);
        runEndMenu.SetActive(false);
        mainMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void SetUIUpgradeMenu()
    {
        creditsMenu.SetActive(false);
        hUDObject.SetActive(false);
        startMenu.SetActive(false);
        runEndMenu.SetActive(false);
        mainMenu.SetActive(false);
        upgradeMenu.SetActive(true);
    }

    public void SetUIRunEndMenu()
    {
        creditsMenu.SetActive(false);
        hUDObject.SetActive(false);
        startMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        mainMenu.SetActive(false);
        runEndMenu.SetActive(true);
        gameManager.CalculateResults();
        gameManager.soundManager.music.clip = gameManager.soundManager.mainMusic;
        gameManager.soundManager.music.Play();
    }

    public void SetUIGamePlay()
    {
        creditsMenu.SetActive(false);
        startMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        runEndMenu.SetActive(false);
        mainMenu.SetActive(false);
        hUDObject.SetActive(true);
        gameManager.soundManager.music.clip = gameManager.soundManager.gameMusic;
        gameManager.soundManager.music.Play();
    }

    public void SetUICredits()
    {
        startMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        runEndMenu.SetActive(false);
        mainMenu.SetActive(false);
        hUDObject.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void UpdateStats()
    {
        string formatString = "\n Armor = {0} \n \n Speed = {1} \n \n Damage = {2}";
        statsDisplay.text = string.Format(formatString, gameManager.playerController.playerStats.maxHP, gameManager.playerController.playerStats.baseMoveSpeed, gameManager.playerController.playerStats.baseDamage);
        if(gameManager.playerController.playerStats.baseMoveSpeed >= gameManager.playerController.playerStats.maxMoveSpeed)
        {
            speedUpButton.interactable = false;
        }
        if(gameManager.playerController.playerStats.maxHP >= 500)
        {
            armorUpButton.interactable = false;
        }
        if(gameManager.playerController.playerStats.baseDamage >= gameManager.playerController.playerStats.maxDamage)
        {
            damageUpButton.interactable = false;
        }
    }

    public void UpdateResults()
    {
        var timespan = TimeSpan.FromSeconds(gameManager.runTime);
        runTimeText.text = string.Format("Run Time = {0}", timespan.ToString(@"mm\:ss"));
        killCountText.text = string.Format("Kills = {0}",gameManager.killCount);
        bonusText.text = string.Format("Bonus = {0}", -gameManager.roundBonus);
        totalText.text = string.Format("TOTAL = {0:.}",gameManager.totalEarned);
    }

    void CanYouAfordTheUpgrade()
    {
        if(gameManager.playerController.playerStats.upgradePoints < 100)
        {
            speedUpButton.interactable = false;
            armorUpButton.interactable = false;
            damageUpButton.interactable = false;
        }
        else
        {
            speedUpButton.interactable = true;
            armorUpButton.interactable = true;
            damageUpButton.interactable = true;
        }
    }

    

}
