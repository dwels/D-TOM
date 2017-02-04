using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AutoTurret : MonoBehaviour
{

    // Variable Declaration
    public Rigidbody Projectile = null;
    public Transform Launcher = null;
    private const float SPAWN_DISTANCE = -0.1f;
    public int power = 50;
    public bool rocketTrue;

    public float reloadTime = 1.0f;
    private float timeLast = 0.0f;



    // Use this for initialization
    void Start()
    {
        rocketTrue = true;
    }

    void Update()
    {
        if (Time.time - timeLast > reloadTime)
        {
            rocketTrue = true;
            FireProjectile(0);
            timeLast = Time.time;
        }//reload time

    }

    void FireProjectile(int type)
    {
        Rigidbody clone;
        if (type == 0)
        {
            clone = Instantiate(Projectile, Launcher.transform.position + (SPAWN_DISTANCE * Launcher.transform.forward), Launcher.transform.rotation) as Rigidbody;
        }
        else
        {
            clone = Instantiate(Projectile, Launcher.transform.position + (SPAWN_DISTANCE * Launcher.transform.forward), Launcher.transform.rotation) as Rigidbody;
        }
        clone.velocity = transform.TransformDirection(-Vector3.forward * power);
        Explode explo = (Explode)clone.gameObject.AddComponent(typeof(Explode)); //clone.AddComponent<Explode>();
        //Destroy(clone);
    }
}
