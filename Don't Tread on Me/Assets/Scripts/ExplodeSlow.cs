using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSlow : MonoBehaviour {

    public Rigidbody Projectile = null;

    float radius = 12f;
    Vector3 pos;

    float upTime = 5f;
    private float timeLast = 0.0f;

    private Collider[] colliders;

    bool activation;

    // Use this for initialization
    void Start () {
        activation = false;
        timeLast = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        if (activation)
        {

            foreach (Collider hit in colliders)
            {

                if ((hit.gameObject.GetComponent("EnemyTank") as EnemyTank) != null)
                {
                    //hit.GetComponent<EnemyTank>().speed = 4f;
                    //hit.GetComponent<EnemyTank>().reloadTime = 6f;
                    //hit.GetComponent<EnemyTank>().rotateSpeed = 1f;
                    hit.GetComponent<EnemyTank>().slowed = true;
                }

                if ((hit.gameObject.GetComponent("EnemyTank") as EnemyTank) != null && Time.time - timeLast > upTime)
                {
                    hit.GetComponent<EnemyTank>().slowed = false;
                    //hit.GetComponent<EnemyTank>().speed = 8f;
                    //hit.GetComponent<EnemyTank>().reloadTime = 3f;
                    //hit.GetComponent<EnemyTank>().rotateSpeed = 2.5f;
                    Destroy(this.gameObject);
                }

            }
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        colliders = Physics.OverlapSphere(transform.position, radius);

        // Constraints Projectiles pos
        Projectile.constraints = RigidbodyConstraints.FreezePosition;

        activation = true;
    }

}
