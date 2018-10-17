using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //TODO Consider for other Player Controllers
    [Range(1,4)]
    public int playerNumber = 1;
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
    string horizontalInputName;
    string verticalInputName;
    string fireInputName;

    float horizontalInput = 0;
    float verticalInput = 0;
    bool fireInput;

    Vector3 shootVector;

    string playerTag = "";
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        maxAmmoAmount = ammoAmount;
        maxReloadTime = reloadTime;
    }
    // Use this for initialization
    void Start ()
    {
        SetInputNames();
        GetInput();
	}
    // Update is called once per frame
    void Update ()
    {
        GetInput();
        if (!autoFire)
        {
            Aim(horizontalInput, verticalInput);
            if (fireInput)
            {
                Shoot();
            }

            //if (playerTag == "Player1")
            //{
               
            //}
            //if(playerTag == "Player2")
            //{
            //    Aim(horizontalInput_P2,verticalInput_P2);
            //}

          
            //if (playerTag == "Player2" && fireInput_P2)
            //{
            //    Shoot();
            //}
        }
        else
        {
            //if (playerTag == "Player1")
            //{
            //    Aim(horizontalInput_P1, verticalInput_P1);
            //}
            //if (playerTag == "Player2")
            //{
            //    Aim(horizontalInput_P2, verticalInput_P2);
            //}
            //here you put without Aiming , it will fire as you press a direction keys
        }
	}
    private void SetInputNames()
    {
        if (isJoystickEnabled)
        {
            horizontalInputName = "HorizontalJoystick_P" + playerNumber;
            verticalInputName = "VerticalJoystick_P" + playerNumber;

            fireInputName = "FireJoystick_P" + playerNumber;
        }
        else
        {
            horizontalInputName = "Horizontal_P" + playerNumber;
            verticalInputName = "Vertical_P" + playerNumber;

            fireInputName = "Fire_P" + playerNumber;
        }
    }
    void GetInput()
    {
        if (isJoystickEnabled)
        {
            horizontalInput = hInput.GetAxis(horizontalInputName);
            verticalInput = hInput.GetAxis(verticalInputName);

            fireInput = hInput.GetButtonDown(fireInputName);
        }
        else
        {
            horizontalInput = hInput.GetAxis(horizontalInputName);
            verticalInput = hInput.GetAxis(verticalInputName);

            fireInput = hInput.GetButtonDown(fireInputName);
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
