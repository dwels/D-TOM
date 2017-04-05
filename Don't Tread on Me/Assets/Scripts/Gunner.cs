using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class Gunner : PlayerRoles {
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

    // Use this for initialization
    void Start () {
        oldRotation = this.transform.rotation;

        // this code is for managing player roles
        GameObject inputMngr = GameObject.Find("InputManager");
        gunner = inputMngr.GetComponent<PlayerRoles>().gunner;

        // pull in rockets script and active reload
        rockets = GetComponent<rockets>();
        activeReload = GetComponent<ActiveReload>();

        // init UI
        anim = gunnerPanel.GetComponent<Animator>();
        anim.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        #region tank top rotation
        //turn Top of Tank to the right
        if (amGunner)
        {
            if (InputManager.GetAxis("Right Stick Horizontal", gunner) != 0.0f)
            {
                this.transform.Rotate(0f, (InputManager.GetAxis("Right Stick Horizontal", gunner) * gunnerRotateSpeed), 0f);
                oldRotation = this.transform.rotation;
            }
        }

        // 100.0f is hardcoded until I figure how to properly deal with parent child rotation
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, oldRotation, Time.deltaTime * 100.0f); 
        #endregion

        #region cannon angle
        //point cannon up
        if (InputManager.GetAxis("Left Stick Vertical", gunner) < 0)
        {
            if (angleCurrent < 100)
            {
                Cannon.transform.RotateAround(cannonPivot.transform.position, cannonPivot.transform.right, -10 * Time.deltaTime);
                angleCurrent++;
            }
        }

        //point cannon down
        if (InputManager.GetAxis("Left Stick Vertical", gunner) > 0)
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

            if (InputManager.GetAxis("Right Trigger", gunner) == 1)
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
    }
}
