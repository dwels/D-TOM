using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class rockets : MonoBehaviour {

    // Variable Declaration
    public Rigidbody Projectile = null;
    public Transform Launcher = null;
    private const float SPAWN_DISTANCE = 1.0f;
    public int power = 50;
	public bool rocketTrue;

    public float reloadTime = 0.5f;
    private float timeLast = 0.0f;



    // Use this for initialization
    void Start()
    {
		rocketTrue = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetAxis("RightTrigger") > 0)
	    {
            if(Time.time - timeLast > reloadTime)
            {
				rocketTrue = true;
				FireProjectile(0);
                timeLast = Time.time;
            }//reload time
	    }
		if (Input.GetMouseButton(1))
		{
			if(Time.time - timeLast > reloadTime)
			{
				rocketTrue = false;
				FireProjectile(1);
				timeLast = Time.time;
			}//reload time
		}
		
	}
	
	void FireProjectile(int type)
	{
		Rigidbody clone;
		if (type == 0) 
		{
			clone = Instantiate (Projectile, Launcher.transform.position + (SPAWN_DISTANCE * Launcher.transform.forward), Launcher.transform.rotation) as Rigidbody;
		} 
		else 
		{
			clone = Instantiate (Projectile, Launcher.transform.position + (SPAWN_DISTANCE * Launcher.transform.forward), Launcher.transform.rotation) as Rigidbody;
		}
        clone.velocity = transform.TransformDirection(Vector3.forward * power);
        Explode explo = (Explode)clone.gameObject.AddComponent(typeof(Explode)); //clone.AddComponent<Explode>();
        //Destroy(clone);
    }
}
