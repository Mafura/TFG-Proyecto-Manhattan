using UnityEngine;
using System.Collections;

public class CameraTriggerController : MonoBehaviour
{
    private GameObject camera;
    private GameObject player;
    private GameObject spawnerUp;
    private GameObject spawnerDown;
    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        spawnerUp = GameObject.FindGameObjectWithTag("SpawnerUp");
        spawnerDown = GameObject.FindGameObjectWithTag("SpawnerDown");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "TriggerEscDown")
        {
            player.transform.position = spawnerUp.transform.position;
            player.GetComponent<PlayerMovement>().direction = PlayerMovement.Direction.DOWN;
        }
        else {
            player.transform.position = spawnerDown.transform.position;
            player.GetComponent<PlayerMovement>().direction = PlayerMovement.Direction.DOWN;
        }
        //player.transform.position = 
    }

    public void OnCollisionStay2D(Collision2D collision)
    {

    }
}
