using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] targets;
    public Transform[] StairSpawners;
    public GameObject player;
    //private Rigidbody2D rigidbody;
    private Bounds currentBounds;
    private float alignDuration = 1f;

    IEnumerator AlignToNewBounds()
    {
        Vector3 startVect = transform.position;
        Vector3 trackingVect = transform.position;

        float targX = currentBounds.center.x;
        float targY = currentBounds.center.y;
        Vector3 targetPosition = new Vector3(targX, targY, transform.position.z);

        float lerpTime = 0;
        while(lerpTime < alignDuration)
        {
            lerpTime += Time.deltaTime;
            trackingVect = Vec3Lerp(lerpTime, alignDuration, startVect, targetPosition);
            transform.position = trackingVect;
            yield return 0;
        }

        transform.position = targetPosition;
    }

    public void SetNewBounds(Bounds newBounds)
    {
        currentBounds = newBounds;
        StartCoroutine(AlignToNewBounds());
    }

    public static Vector3 Vec3Lerp(float currentTime, float duration, Vector3 v3_start, Vector3 v3_target)
    {
        float step = (currentTime / duration);
        Vector3 v3_ret = Vector3.Lerp(v3_start, v3_target, step);
        return v3_ret;
    }

    /*public void Move(GameObject trigger, GameObject escenery1, GameObject escenery2)
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
    }*/

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
