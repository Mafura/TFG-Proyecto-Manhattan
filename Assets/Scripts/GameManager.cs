using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float timeStamp = 0.0f;
    public int timeHours = 0;
    public int day = 1;
    private PlayerMovement player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.timeFlow)
        {
            timeStamp += Time.deltaTime;
            timeHours = (int)(timeStamp / 5);
            if(timeHours / 48 == 1)
            {
                day += 1;
                timeStamp = 0.0f;
                timeHours = 0;
            }
        }
	}
}
