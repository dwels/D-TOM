using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class Commander : MonoBehaviour {

    public GameObject target;
    public GameObject crate;
    public GameObject reticle;

    public float translateSpeed = 10.0f;
    public float crateHeight = 100.0f;
    public float timerCap = 1.0f;

    private Vector3 oldTargetPosition;
    private float timerLast = 0.0f;

    GameObject inputMngr;
    public PlayerID playerID;

    // Use this for initialization
    void Start () {

        oldTargetPosition = target.transform.position;

        inputMngr = GameObject.Find("InputManager");
        playerID = inputMngr.GetComponent<PlayerRoles>().commander;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerID != inputMngr.GetComponent<PlayerRoles>().commander) return;

        // follow the tank
        Vector3 follow = target.transform.position - oldTargetPosition;
        reticle.transform.Translate(follow, Space.World);

        // movement input
        if (InputManager.GetAxis("Left Stick Vertical", playerID) != 0.0f)
        {
            reticle.transform.Translate(new Vector3(1, 0, 1) * -InputManager.GetAxis("Left Stick Vertical", playerID) * translateSpeed  * Time.deltaTime, Space.World);
        }
        if (InputManager.GetAxis("Left Stick Horizontal", playerID) != 0.0f)
        {
            reticle.transform.Translate(new Vector3(-1, 0, 1) * -InputManager.GetAxis("Left Stick Horizontal", playerID) * translateSpeed * Time.deltaTime, Space.World);
        }

        // update tank's old position
        oldTargetPosition = target.transform.position;

        if(InputManager.GetAxis("Left Trigger", playerID) > 0)
        {
            if (Time.time - timerLast > timerCap)
            {
                Vector3 crateStart = reticle.transform.position;
                crateStart.y = crateHeight;
                Instantiate(crate, crateStart, Quaternion.identity);
                timerLast = Time.time;
            }
        }

        if (InputManager.GetAxis("DPAD Vertical", playerID) == 1)
        {
            Gunner gunner = GetComponent<Gunner>();
            gunner.playerID = playerID;
            playerID = inputMngr.GetComponent<PlayerRoles>().gunner;

            inputMngr.GetComponent<PlayerRoles>().gunner = gunner.playerID;
            inputMngr.GetComponent<PlayerRoles>().commander = playerID;
        }
        else if (InputManager.GetAxis("DPAD Horizontal", playerID) == -1)
        {
            Engineer engineer = GetComponent<Engineer>();
            engineer.playerID = playerID;
            playerID = inputMngr.GetComponent<PlayerRoles>().engineer;

            inputMngr.GetComponent<PlayerRoles>().engineer = engineer.playerID;
            inputMngr.GetComponent<PlayerRoles>().commander = playerID;
        }
    }
}
