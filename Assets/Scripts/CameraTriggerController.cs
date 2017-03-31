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

    }

    public void OnCollisionStay2D(Collision2D collision)
    {

    }
}
