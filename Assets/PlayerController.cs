using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //TODO Consider for other Player Controllers
    public Transform weapon;
    public bool autoFire = false;
    public Transform spawnPoint;
    public GameObject bullet;
    public float ammoAmount;
    public float reloadTime;
    public bool isJoystickEnabled = false;

    public float knockbackRate;

    Rigidbody2D rb2d;

    float maxAmmoAmount;
    float maxReloadTime;

    //Add more for more players
    float horizontalInput_P1 = 0;
    float verticalInput_P1 = 0;
    bool fireInput_P1;

    float horizontalInput_P2 = 0;
    float verticalInput_P2 = 0;
    bool fireInput_P2;

    Vector3 shootVector;

    string playerTag = "";
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        maxAmmoAmount = ammoAmount;
        maxReloadTime = reloadTime;

        //TODO Find something better with this type of code
        if (CompareTag("Player_1"))
        {
            playerTag = "Player1";
        }

        if (CompareTag("Player_2"))
        {
            playerTag = "Player2";
        }
    }
    // Use this for initialization
    void Start ()
    {
        GetInput();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
        if (!autoFire)
        {
            if(playerTag == "Player1")
            {
                Aim(horizontalInput_P1,verticalInput_P1);
            }
            if(playerTag == "Player2")
            {
                Aim(horizontalInput_P2,verticalInput_P2);
            }

            if (playerTag == "Player1" && fireInput_P1)
            {
                Shoot();
            }
            if (playerTag == "Player2" && fireInput_P2)
            {
                Shoot();
            }
        }
        else
        {
            if (playerTag == "Player1")
            {
                Aim(horizontalInput_P1, verticalInput_P1);
            }
            if (playerTag == "Player2")
            {
                Aim(horizontalInput_P2, verticalInput_P2);
            }
            //here you put without Aiming , it will fire as you press a direction keys
        }
	}
    void GetInput()
    {
        if (isJoystickEnabled)
        {
            horizontalInput_P1 = hInput.GetAxis("HorizontalJoystick_P1");
            verticalInput_P1 = hInput.GetAxis("VerticalJoystick_P1");

            horizontalInput_P2 = hInput.GetAxis("HorizontalJoystick_P2");
            verticalInput_P2 = hInput.GetAxis("VerticalJoystick_P2");

            fireInput_P1 = hInput.GetButtonDown("FireJoystick_P1");
            fireInput_P2 = hInput.GetButtonDown("FireJoystick_P2");
        }
        else
        {
            if (playerTag == "Player1")
            {
                horizontalInput_P1 = hInput.GetAxis("Horizontal_P1");
                verticalInput_P1 = hInput.GetAxis("Vertical_P1");

                fireInput_P1 = hInput.GetButtonDown("Fire_P1");
            }
            if(playerTag == "Player2")
            {
                horizontalInput_P2 = hInput.GetAxis("Horizontal_P2");
                verticalInput_P2 = hInput.GetAxis("Vertical_P2");

                fireInput_P2 = hInput.GetButtonDown("Fire_P2");
            }
        }
    }

    void Aim(float horizontal,float vertical)
    {
        //gets the direction the input
        Vector3 aimVector = new Vector3(horizontal, vertical, 0);
        //Check if the user presses an input
        if(aimVector.x != 0 || aimVector.y != 0)
        {
            shootVector = aimVector;
            //Calculate the Angle of the weapon and brings the direction to life.
            float gunAngle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;
            weapon.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle));
            if (autoFire)
            {
                Shoot();
            }
            Debug.Log(aimVector);
        }
    }
    void Shoot()
    {
        Vector3 spawnPos = spawnPoint.position;
        Quaternion spawnRot = Quaternion.identity;

        BaseBulletScript bul = Instantiate(bullet, spawnPos, spawnRot).GetComponent<BaseBulletScript>();
        bul.Setup(shootVector);
        KnockBack(shootVector);
    }

    void KnockBack(Vector3 direction)
    {
        rb2d.AddForce((direction * -knockbackRate) * Time.deltaTime, ForceMode2D.Impulse);
    }
}
