using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [Header("HUD")]
    public GameObject hUDObject;
    public TextMeshProUGUI healthUI;
    public Slider healthSlider;
    public GameManager gameManager;

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
        }
        if(mainMenu.activeSelf)
        {
            loadButton.interactable = gameManager.CheckforSave();
        }
        if(upgradeMenu.activeSelf)
        {
            CanYouAfordTheUpgrade();
            UpdateStats();
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
    }

    public void SetUIGamePlay()
    {
        creditsMenu.SetActive(false);
        startMenu.SetActive(false);
        upgradeMenu.SetActive(false);
        runEndMenu.SetActive(false);
        mainMenu.SetActive(false);
        hUDObject.SetActive(true);
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
