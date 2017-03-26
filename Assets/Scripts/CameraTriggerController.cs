using UnityEngine;
using System.Collections;

public class CameraTriggerController : MonoBehaviour
{
    private GameObject camera;
    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "TriggerEsc")
        {
            camera.GetComponent<CameraController>().MoveStairs(gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(gameObject.tag == "TriggerH" || gameObject.tag == "TriggerV")
        {
            camera.GetComponent<CameraController>().Move(gameObject);
        }
    }
}
