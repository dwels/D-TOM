using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class Commander : MonoBehaviour {

    private PlayerID commander;

    public GameObject target;
    public GameObject crate;
    public float translateSpeed = 10.0f;
    public float crateHeight = 100.0f;
    public float timerCap = 1.0f;

    private Vector3 oldTargetPosition;
    private float timerLast = 0.0f;

	// Use this for initialization
	void Start () {

        oldTargetPosition = target.transform.position;

        // this code is for managing player roles
        GameObject inputMngr = GameObject.Find("InputManager");
        commander = inputMngr.GetComponent<PlayerRoles>().commander;
    }
	
	// Update is called once per frame
	void Update () {

        // follow the tank
        Vector3 follow = target.transform.position - oldTargetPosition;
        transform.Translate(follow, Space.World);

        // movement input
        if (InputManager.GetAxis("Left Stick Vertical", commander) != 0.0f)
        {
            transform.Translate(new Vector3(1, 0, 1) * -InputManager.GetAxis("Left Stick Vertical", commander) * translateSpeed  * Time.deltaTime, Space.World);
        }
        if (InputManager.GetAxis("Left Stick Horizontal", commander) != 0.0f)
        {
            transform.Translate(new Vector3(-1, 0, 1) * -InputManager.GetAxis("Left Stick Horizontal", commander) * translateSpeed * Time.deltaTime, Space.World);
        }

        // update tank's old position
        oldTargetPosition = target.transform.position;

        if(InputManager.GetAxis("Left Trigger", commander) > 0)
        {
            if (Time.time - timerLast > timerCap)
            {
                Vector3 crateStart = transform.position;
                crateStart.y = crateHeight;
                Instantiate(crate, crateStart, Quaternion.identity);
                timerLast = Time.time;
            }
        }
    }
}
