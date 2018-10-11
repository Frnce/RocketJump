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

    public float knockbackRate;

    Rigidbody2D rb2d;

    float maxAmmoAmount;
    float maxReloadTime;
    float horizontalInput = 0;
    float verticalInput = 0;
    bool fireInput;

    Vector3 shootVector;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        maxAmmoAmount = ammoAmount;
        maxReloadTime = reloadTime;
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
            Aim();
            if (fireInput)
            {
                Shoot();
            }
        }
        else
        {
            Aim();
            //here you put without Aiming , it will fire as you press a direction keys
        }
	}
    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        fireInput = Input.GetKeyDown(KeyCode.K);
    }

    void Aim()
    {
        //gets the direction the input
        Vector3 aimVector = new Vector3(horizontalInput, verticalInput, 0);
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
