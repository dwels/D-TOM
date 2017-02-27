using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : MonoBehaviour {

    public int speed = 10;
    public int rotateSpeed = 100;
    public GameObject TankTop;
    public GameObject Cannon;
    public GameObject cannonPivot;
    private float modSpeed;
    private float modRotateSpeed;
    private int angleCurrent;

    public float HP = 100;

    public int BoostTotal = 300; //5 seconds of boost
    private int BoostFrames = 300;
    private int i;
    public int boostCooldown = 60;
    private int framesSinceLastBoost;
    private bool canBoost;

    public bool amDriver;
    private Rigidbody rb;
    public GameObject rightTreadPivot;
    public GameObject leftTreadPivot;



    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (amDriver) //if driver
        {
            #region driver
            #region boost what a fucking mess
            //driver's boost
            if (Input.GetAxis("RightTrigger") == 1 && Input.GetAxis("LeftTrigger") == 1) //are the boost buttons being pressed?
            {
                if (BoostFrames > 0 && canBoost == true) //is there boost left, and is it on cooldown?
                {
                    //then go fast
                    speed = 10 * 2;
                    rotateSpeed = 100 * 2;

                    //but reduce the boost pool
                    BoostFrames--;
                }//if
                else
                {
                    speed = 10;
                    rotateSpeed = 100;
                }
            }//if 
            else if (Input.GetAxis("RightTrigger") != 1 || Input.GetAxis("LeftTrigger") != 1)
            {
                speed = 10;
                rotateSpeed = 100;

                i++;
                if (i >= 3)
                {
                    if (BoostFrames <= BoostTotal)
                    {
                        BoostFrames++;
                    }
                    i = 0;
                }
            }

            if (BoostFrames == 0)
            {
                canBoost = false;
            }
            if (canBoost == false)
            {
                if (BoostFrames >= boostCooldown)
                {
                    canBoost = true;
                }
            }
            #endregion

            //rotate clockwise
            if (Input.GetAxis("LeftThumbStick") < 0 && Input.GetAxis("RightThumbVertical") > 0)
            {
                Quaternion deltaRotation = Quaternion.Euler((new Vector3(0, 1, 0) * (rotateSpeed)) * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            //rotate counterclockwise
            if (Input.GetAxis("LeftThumbStick") > 0 && Input.GetAxis("RightThumbVertical") < 0)
            {
                Quaternion deltaRotation = Quaternion.Euler((new Vector3(0, -1, 0) * (rotateSpeed)) * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            //move forward
            if (Input.GetAxis("LeftThumbStick") < 0 && Input.GetAxis("RightThumbVertical") < 0)
            {
                    rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
            }

            //move backward
            if (Input.GetAxis("LeftThumbStick") > 0 && Input.GetAxis("RightThumbVertical") > 0)
            {
                    rb.MovePosition(transform.position + -transform.forward * Time.deltaTime * speed);
            }

            //left stick only
            if (Input.GetAxis("LeftThumbStick") < 0 && Input.GetAxis("RightThumbVertical") == 0)
            {
                rotateRigidBodyAroundPointBy(rb, rightTreadPivot.transform.position, rightTreadPivot.transform.up, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("LeftThumbStick") > 0 && Input.GetAxis("RightThumbVertical") == 0)
            {
                rotateRigidBodyAroundPointBy(rb, rightTreadPivot.transform.position, rightTreadPivot.transform.up, -(rotateSpeed * Time.deltaTime));
            }

            //right stick only
            if (Input.GetAxis("LeftThumbStick") == 0 && Input.GetAxis("RightThumbVertical") > 0)
            {
                rotateRigidBodyAroundPointBy(rb, leftTreadPivot.transform.position, leftTreadPivot.transform.up, (rotateSpeed * Time.deltaTime));
            }
            if (Input.GetAxis("LeftThumbStick") == 0 && Input.GetAxis("RightThumbVertical") < 0)
            {
                rotateRigidBodyAroundPointBy(rb, leftTreadPivot.transform.position, leftTreadPivot.transform.up, -(rotateSpeed * Time.deltaTime));
            }
            #endregion
        }
        else // else
        {
            #region default movement
            // Paul: commented out for testing with Commander
            /*
            //move forward - currently very shitty on keyboard
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetAxis("LeftThumbStick") < 0)
            {
                modSpeed = speed * -Input.GetAxis("LeftThumbStick");
                transform.Translate(Vector3.forward * modSpeed * Time.deltaTime);
            }

            //move backward - see move forward
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            if (Input.GetAxis("LeftThumbStick") > 0)
            {
                modSpeed = speed * Input.GetAxis("LeftThumbStick");
                transform.Translate(Vector3.back * modSpeed * Time.deltaTime);
            }

            //rotate left
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0f, (-1 * rotateSpeed), 0f);
            }
            if (Input.GetAxis("RightThumbStick") < 0)
            {
                modRotateSpeed = rotateSpeed * -Input.GetAxis("RightThumbStick");
                transform.Rotate(0f, (-1 * modRotateSpeed), 0f);
            }

            //rotate right
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0f, (1 * rotateSpeed), 0f);
            }
            if (Input.GetAxis("RightThumbStick") > 0)
            {
                modRotateSpeed = rotateSpeed * Input.GetAxis("RightThumbStick");
                transform.Rotate(0f, (1 * modRotateSpeed), 0f);
            }
            */
            #endregion

            #region tank top rotation
            //turn Top of Tank to the right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("DPadLeftRight") > 0)
            {
                TankTop.transform.Rotate(0f, (1 * rotateSpeed), 0f);
            }

            //turn Top of Tank to the left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("DPadLeftRight") < 0)
            {
                TankTop.transform.Rotate(0f, (-1 * rotateSpeed), 0f);
            }
            #endregion

            #region cannon angle
            //point cannon up
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("DPadUpDown") > 0)
            {
                if (angleCurrent < 100)
                {
                    Cannon.transform.RotateAround(cannonPivot.transform.position, cannonPivot.transform.right, -10 * Time.deltaTime);
                    angleCurrent++;
                }
            }

            //point cannon down
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("DPadUpDown") < 0)
            {
                if (angleCurrent > -80)
                {
                    Cannon.transform.RotateAround(cannonPivot.transform.position, cannonPivot.transform.right, 10 * Time.deltaTime);
                    angleCurrent--;
                }
            }
            #endregion
        }
    }

    public void TakeDamage(float damage)
    {
        //subtract damage from HP
        HP -= damage;
        // print("Current HP: " + HP);

        //if no HP
        if (HP <= 0)
        {
            //destroy the tank
            print("She's dead, Jim");
        }
    }//take damage

    public void rotateRigidBodyAroundPointBy(Rigidbody rb, Vector3 origin, Vector3 axis, float angle)
    {
        //rb is what is rotating
        //origin is the point around which to rotate
        //axis is the axis we are rotating around
        //angle is how much, so use rotspeed * deltatime?
        Quaternion q = Quaternion.AngleAxis(angle, axis);
        rb.MovePosition(q * (rb.transform.position - origin) + origin);
        rb.MoveRotation(rb.transform.rotation * q);
    }//rotate rigidbody around point
}
