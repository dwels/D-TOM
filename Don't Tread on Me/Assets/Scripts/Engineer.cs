using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class Engineer : MonoBehaviour {
    public bool amEngineer;

    public GameObject Hull;
    public Rigidbody Projectile = null;
    public GameObject tankTop;
    Camera mainCamera;
    private const float SPAWN_DISTANCE = 2f;

    float throwPower;
    public float throwRate;
    float maxPower;

    HP hp;
    GameObject pTnk;
    bool repairing;
    int repairAmount = 1;

    GameObject inputMngr;
    public PlayerID playerID;

    // Use this for initialization
    void Start () {
        throwRate = 8f;
        maxPower = 8f;
        repairing = false;

        pTnk = GameObject.Find("Player");
        hp = pTnk.GetComponent<HP>();

        // this code is for managing player roles
        inputMngr = GameObject.Find("InputManager");
        playerID = inputMngr.GetComponent<PlayerRoles>().engineer;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerID != inputMngr.GetComponent<PlayerRoles>().engineer) return;

        if (amEngineer)
        {

            if (!repairing)
            {        
                //print(Input.GetAxis("LeftTrigger"));
                if (InputManager.GetButton("Button B", playerID))
                {
                    if (throwPower <= maxPower)
                    {
                        throwPower += Time.deltaTime * throwRate;
                    }
                }
                else if (InputManager.GetButtonUp("Button B", playerID))
                {
                    ThrowGrenade(throwPower);
                    throwPower = 0f;
                }
            }

            // Hold to repair
            if(InputManager.GetAxis("Left Trigger", playerID) > 0)
            {
                repairing = true;
                //playerTank.HP += Time.deltaTime * 2f;
                //print(playerTank.HP);
            }  
            // OR Mash a button to repair (while holding LeftTrigger to disable use of other actions)
            /*if (Input.GetButtonDown("A") || repairing)
            {
                playerTank.HP += repairAmount;
                print(playerTank.HP);
            }*/
        }

        if (InputManager.GetAxis("DPAD Vertical", playerID) == 1)
        {
            Gunner gunner = GetComponent<Gunner>();
            gunner.playerID = playerID;
            playerID = inputMngr.GetComponent<PlayerRoles>().gunner;

            inputMngr.GetComponent<PlayerRoles>().gunner = gunner.playerID;
            inputMngr.GetComponent<PlayerRoles>().engineer = playerID;
        }
        else if (InputManager.GetAxis("DPAD Vertical", playerID) == -1)
        {
            Driver driver = GetComponent<Driver>();
            driver.playerID = playerID;
            playerID = inputMngr.GetComponent<PlayerRoles>().driver;

            inputMngr.GetComponent<PlayerRoles>().driver = driver.playerID;
            inputMngr.GetComponent<PlayerRoles>().engineer = playerID;
        }
        else if (InputManager.GetAxis("DPAD Horizontal", playerID) == 1)
        {
            Commander commander = GetComponent<Commander>();
            commander.playerID = playerID;
            playerID = inputMngr.GetComponent<PlayerRoles>().commander;

            inputMngr.GetComponent<PlayerRoles>().commander = commander.playerID;
            inputMngr.GetComponent<PlayerRoles>().engineer = playerID;
        }

    }


    void ThrowGrenade(float tPWR)
    {
        Rigidbody clone = Instantiate(Projectile, Hull.transform.position + (SPAWN_DISTANCE * Hull.transform.forward), tankTop.transform.rotation) as Rigidbody;  //Hull.transform.rotation) as Rigidbody;

        Vector3 temp = new Vector3(0, 1, 1);

        clone.velocity = tankTop.transform.TransformDirection( temp  * tPWR);

        Explode explo = (Explode)clone.gameObject.AddComponent(typeof(Explode));
    }


}
