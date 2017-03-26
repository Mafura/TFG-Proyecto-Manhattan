using UnityEngine;
using System.Collections;
using Fungus;

public class NPCController : MonoBehaviour
{
    public bool talkable = false;
    public Flowchart flowchart;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(flowchart.GetComponent<>)
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkable = true;
            Flowchart aux = collision.GetComponent<PlayerMovement>().flowchart;
            if(aux != flowchart)
            {
                collision.GetComponent<PlayerMovement>().flowchart = flowchart;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkable = false;

            
        }
    }


}
