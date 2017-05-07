using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public int score;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScore(int points)
    {
        score += points;
        UpdateScore();
    }

    public int GetScore()
    {
        return score;
    }

    void UpdateScore()
    {
        // Updates score UI
        Debug.Log("Score: " + score);
    }

}

