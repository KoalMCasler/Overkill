using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Object Referances")]
    [SerializeField]
    private GameObject[] bodys = new GameObject[3];
    [SerializeField]
    private GameObject[] weapons = new GameObject[3];
    [SerializeField]
    private GameObject activeWeapon;
    private Rigidbody2D weaponRb;
    [SerializeField]
    private GameObject activeBody;
    [SerializeField]
    private Rigidbody2D rb;
    private Vector2 mousePosition;
    [SerializeField]
    private Transform[] firePoints = new Transform[2];
    [SerializeField]
    private GameObject projectile;
    public InputActionAsset playerInputs;
    private InputAction fireAction;
    public GameManager gameManager;
    [Header("Stats")]
    [SerializeField]
    public Stats playerStats;
    private Vector2 moveDirection;
    public float fireForce = 30f;
    private bool hasFiredLeft;
    private bool hasFiredRight;
    public float activeShotDelay;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        gameManager = GameManager.gameManager;
    }
    // Start is called before the first frame update
    void Start()
    {
        fireAction = playerInputs.FindAction("Fire", false);
        hasFiredLeft = false;
        hasFiredRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse input
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckForDeath();
    }

    void OnMove(InputValue movementValue)
    {
        if(playerStats.isAlive)
        {
            //Movement logic
            Vector2 moveVector2 = movementValue.Get<Vector2>();
            moveDirection = moveVector2;
        }
    }
    private void FixedUpdate()
    {
        // this moves the player indepented from aim direction
        rb.velocity = new Vector2(moveDirection.x * playerStats.moveSpeed, moveDirection.y * playerStats.moveSpeed);
        //weaponRb.velocity = new Vector2(moveDirection.x * playerStats.moveSpeed, moveDirection.y * playerStats.moveSpeed);
        activeWeapon.transform.position = this.transform.position;
        Vector2 aimDirection = mousePosition - rb.position;
        
        // This horrible looking math is how the character stays looking at the mouse point
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        weaponRb.rotation = aimAngle;   

        if(fireAction.IsPressed() && playerStats.isAlive)
        {
            FireWeapon();
        }
    }

    public void SetForm(string disredForm)
    {
        if(disredForm == "Blue" || disredForm == "Red" || disredForm == "Green")
        {
            playerStats.playerForm = disredForm;
        }
    }

    public void LoadForm()
    {
        if(playerStats.playerForm == "Blue" && activeBody == null && activeWeapon == null)
        {
            activeBody = Instantiate(bodys[0], this.transform);
            activeWeapon = Instantiate(weapons[0], this.transform);
            firePoints[0] = activeWeapon.transform.GetChild(0);
            firePoints[1] = activeWeapon.transform.GetChild(1);
        }
        else if(playerStats.playerForm == "Green" && activeBody == null && activeWeapon == null)
        {
            activeBody = Instantiate(bodys[1], this.transform);
            activeWeapon = Instantiate(weapons[1], this.transform);
            firePoints[0] = activeWeapon.transform.GetChild(0);
        }
        else if(playerStats.playerForm == "Red" && activeBody == null && activeWeapon == null)
        {
            activeBody = Instantiate(bodys[2], this.transform);
            activeWeapon = Instantiate(weapons[2], this.transform);
            firePoints[0] = activeWeapon.transform.GetChild(0);
        }
        playerStats.shotDelay = playerStats.baseShotDelay;
        playerStats.damage = playerStats.baseDamage;
        playerStats.moveSpeed = playerStats.baseMoveSpeed;
        weaponRb = activeWeapon.GetComponent<Rigidbody2D>();
        playerStats.currentHP = playerStats.maxHP;
    }

    public void CheckForm()
    {
        Destroy(activeBody);
        Destroy(activeWeapon);
        if(playerStats.playerForm == "Blue")
        {
            activeBody = Instantiate(bodys[0], this.transform);
            activeWeapon = Instantiate(weapons[0], this.transform);
            firePoints[0] = activeWeapon.transform.GetChild(0);
            firePoints[1] = activeWeapon.transform.GetChild(1);
            playerStats.baseShotDelay = .25f;
            playerStats.baseDamage = .75f;
            playerStats.baseMoveSpeed = 6f;
        }
        else if(playerStats.playerForm == "Green")
        {
            activeBody = Instantiate(bodys[1], this.transform);
            activeWeapon = Instantiate(weapons[1], this.transform);
            firePoints[0] = activeWeapon.transform.GetChild(0);
            playerStats.baseShotDelay = .5f;
            playerStats.baseDamage = 1f;
            playerStats.baseMoveSpeed = 5f;
        }
        else if(playerStats.playerForm == "Red")
        {
            activeBody = Instantiate(bodys[2], this.transform);
            activeWeapon = Instantiate(weapons[2], this.transform);
            firePoints[0] = activeWeapon.transform.GetChild(0);
            playerStats.baseShotDelay = .75f;
            playerStats.baseDamage = 1.25f;
            playerStats.baseMoveSpeed = 4f;
        }
        playerStats.maxHP = 100;
        playerStats.shotDelay = playerStats.baseShotDelay;
        playerStats.damage = playerStats.baseDamage;
        playerStats.moveSpeed = playerStats.baseMoveSpeed;
        weaponRb = activeWeapon.GetComponent<Rigidbody2D>();
    }

    void FireWeapon()
    {
        activeShotDelay -= Time.deltaTime;
        if(activeShotDelay <= 0)
        {
            if(playerStats.playerForm == "Blue")
            {
                if(hasFiredLeft == false)
                {
                    GameObject Bullet = Instantiate(projectile, firePoints[0].position, firePoints[0].rotation);
                    Bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[0].up * fireForce, ForceMode2D.Impulse);
                    Destroy(Bullet, 3f);
                    hasFiredLeft = true;
                    hasFiredRight = false;
                }
                else if(hasFiredRight == false)
                {
                    GameObject Bullet = Instantiate(projectile, firePoints[1].position, firePoints[1].rotation);
                    Bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[1].up * fireForce, ForceMode2D.Impulse);
                    Destroy(Bullet, 3f);
                    hasFiredLeft = false;
                    hasFiredRight = true;
                }
            }
            else
            {
                GameObject Bullet = Instantiate(projectile, firePoints[0].position, firePoints[0].rotation);
                Bullet.GetComponent<Rigidbody2D>().AddForce(firePoints[0].up * fireForce, ForceMode2D.Impulse);
                Destroy(Bullet, 3f);
            }
            activeShotDelay = playerStats.shotDelay;
        }
    }

    void CheckForDeath()
    {
        if(playerStats.currentHP <= 0 && playerStats.isAlive)
        {
            playerStats.isAlive = false;
            gameManager.roundBonus = playerStats.currentHP;
            
            gameManager.uIManager.SetUIRunEndMenu();
        }
    }
    
}
