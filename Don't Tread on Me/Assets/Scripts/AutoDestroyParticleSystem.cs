using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour {

    private ParticleSystem psys;

	// Use this for initialization
	void Start () {
        psys = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (psys)
        {
            if (!psys.IsAlive())
            {
                Destroy(gameObject);
            }
        }
	}
}
