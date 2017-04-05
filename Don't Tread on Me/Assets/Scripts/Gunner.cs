using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class Gunner : MonoBehaviour {
    public bool amGunner;

    public GameObject Cannon;
    public GameObject cannonPivot;
    private int angleCurrent;

    // for gunner rotation
    Quaternion oldRotation;
    public float gunnerRotateSpeed = 1.0f;

    // for main gunner and active reload
    public GameObject Launcher;
    private rockets rockets = null;
    private ActiveReload activeReload = null;

    // for UI
    // http://www.thegamecontriver.com/2014/10/create-sliding-pause-menu-unity-46-gui.html
    public GameObject gunnerPanel;
    private Animator anim;

    GameObject inputMngr;
    public PlayerID playerID;

    // Use this for initialization
    void Start () {
        oldRotation = this.transform.rotation;

        // pull in rockets script and active reload
        rockets = GetComponent<rockets>();
        activeReload = GetComponent<ActiveReload>();

        // init UI
        anim = gunnerPanel.GetComponent<Animator>();
        anim.enabled = true;

        inputMngr = GameObject.Find("InputManager");
        playerID = inputMngr.GetComponent<PlayerRoles>().gunner;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerID != inputMngr.GetComponent<PlayerRoles>().gunner) return;

        #region tank top rotation
        //turn Top of Tank to the right
        if (amGunner)
        {
            if (InputManager.GetAxis("Right Stick Horizontal", playerID) != 0.0f)
            {
                this.transform.Rotate(0f, (InputManager.GetAxis("Right Stick Horizontal", playerID) * gunnerRotateSpeed), 0f);
                oldRotation = this.transform.rotation;
            }
        }
        #endregion

        #region cannon angle
        //point cannon up
        if (InputManager.GetAxis("Left Stick Vertical", playerID) < 0)
        {
            if (angleCurrent < 100)
            {
                Cannon.transform.RotateAround(cannonPivot.transform.position, cannonPivot.transform.right, -10 * Time.deltaTime);
                angleCurrent++;
            }
        }

        //point cannon down
        if (InputManager.GetAxis("Left Stick Vertical", playerID) > 0)
        {
            if (angleCurrent > -80)
            {
                Cannon.transform.RotateAround(cannonPivot.transform.position, cannonPivot.transform.right, 10 * Time.deltaTime);
                angleCurrent--;
            }
        }
        #endregion

        #region main gun
        if (!activeReload.IsReloading())
        {
            print("Ready to Fire");
            // draw line
            // ToDo: this should be a raycast to help see what it is aiming at
            Vector3 forward = Launcher.transform.TransformDirection(Vector3.forward) * 20;
            Debug.DrawRay(Launcher.transform.position, forward, Color.red);

            if (InputManager.GetAxis("Right Trigger", playerID) == 1)
            {
                print("Firing");
                // ToDo: ammotype needs to be implemented
                rockets.FireProjectile(0);
                activeReload.Reload();

                // display reload UI
                anim.Play("panelSlideIn");
            }

            else
            {
                // hide UI
                anim.Play("panelSlideOut");
            }
        }
        #endregion

        if (InputManager.GetAxis("DPAD Horizontal", playerID) == 1)
        {
            Commander commander = GetComponent<Commander>();
            commander.playerID = playerID;
            playerID = inputMngr.GetComponent<PlayerRoles>().commander;

            inputMngr.GetComponent<PlayerRoles>().commander = commander.playerID;
            inputMngr.GetComponent<PlayerRoles>().gunner = playerID;
        }
        else if (InputManager.GetAxis("DPAD Horizontal", playerID) == -1)
        {
            Engineer engineer = GetComponent<Engineer>();
            engineer.playerID = playerID;
            playerID = inputMngr.GetComponent<PlayerRoles>().engineer;

            inputMngr.GetComponent<PlayerRoles>().engineer = engineer.playerID;
            inputMngr.GetComponent<PlayerRoles>().gunner = playerID;
        }
    }
}
