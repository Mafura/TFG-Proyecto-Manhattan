using UnityEngine;
using System.Collections;

public class ScreenRegion : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    BoxCollider2D box;

    // Use this for initialization

    public void Awake()
    {
        box = gameObject.GetComponent<BoxCollider2D>();
        rigidbody2d = gameObject.AddComponent<Rigidbody2D>();
        rigidbody2d.isKinematic = true;
    }

    void SetNewCameraBounds()
    {
        CameraController cam = Camera.main.gameObject.GetComponent<CameraController>();
        cam.SetNewBounds(box.bounds);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            SetNewCameraBounds();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
