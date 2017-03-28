using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] targets;
    public Transform[] StairSpawners;
    public GameObject player;
    private Rigidbody2D rigidbody;
    //public GameObject[] triggers;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Move(GameObject trigger, GameObject escenery1, GameObject escenery2)
    {
        if(trigger.tag == "TriggerV")
        {
            float distance = Mathf.Abs(escenery1.transform.position.y - escenery2.transform.position.y);
            float aux = distance / trigger.GetComponent<BoxCollider2D>().size.y;
            if(player.GetComponent<Rigidbody2D>().velocity.y > 0.0f)
            {
                rigidbody.velocity = new Vector2(0, aux);
            }
            else
            {
                rigidbody.velocity = new Vector2(0, -aux);
            }
        }
        else
        {
            float distance = Mathf.Abs(escenery1.transform.position.x - escenery2.transform.position.x);
            float aux = distance / trigger.GetComponent<BoxCollider2D>().size.x;
            if (player.GetComponent<Rigidbody2D>().velocity.x > 0.0f)
            {
                rigidbody.velocity = new Vector2(aux, 0);
            }
            else
            {
                rigidbody.velocity = new Vector2(-aux, 0);
            }
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
