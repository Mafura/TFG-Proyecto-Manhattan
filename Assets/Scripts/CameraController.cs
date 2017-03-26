using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] targets;
    public Transform[] StairSpawners;
    public GameObject player;
    //public GameObject[] triggers;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Move(GameObject trigger)
    {
        if(trigger.tag == "TriggerV")
        {

        }
        else
        {

        }
    }

    public void MoveStairs(GameObject trigger)
    {
        if(trigger.name == "TriggerEscUp")
        {
            gameObject.transform.position = targets[0].position;
            player.GetComponent<PlayerMovement>().SetPosition(StairSpawners[0]);
        }
        else
        {
            gameObject.transform.position = targets[0].position;
            player.GetComponent<PlayerMovement>().SetPosition(StairSpawners[1]);
        }
    }
}
