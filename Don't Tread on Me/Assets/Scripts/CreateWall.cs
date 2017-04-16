using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour {

    public int sizeX;
    public int sizeY;
    public Transform brick;
    public float lifetime = 5.0f;

    private bool triggered = false;
    private List<Transform> bricks = new List<Transform>();

	// Use this for initialization
	void Start () {
		for(int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Transform newBrick = Instantiate(brick, this.transform);
                newBrick.transform.localPosition = new Vector3(
                    x * brick.localScale.x + (brick.localScale.x / 2) - (brick.localScale.x * (sizeX / 2)) - (brick.localScale.x / 2), 
                    y * brick.localScale.y + (brick.localScale.y / 2), 0);
                newBrick.transform.rotation = this.gameObject.transform.rotation;
                bricks.Add(newBrick);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered)
        {
            Destroy(gameObject, lifetime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!triggered)
            {
                foreach (Transform child in bricks)
                {
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }

                triggered = true;
            }
        }

    }
}
