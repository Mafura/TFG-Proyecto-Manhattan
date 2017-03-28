using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Fungus;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 3.0f;
    private Direction direction = Direction.DOWN;
    private Rigidbody2D rigidbody;
    private SpriteRenderer renderer;
    public Sprite[] sprites;
    public Flowchart flowchart;
    private GameObject[] npc;
    private bool speaking = false;
    private Block[] blockList;
    private int index = 1;
    public int blockIndex = 0;
    public bool timeFlow = true;
    
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //rigidbody.mass = 0;
        renderer = GetComponent<SpriteRenderer>();
        
        npc = GameObject.FindGameObjectsWithTag("NPC");
        flowchart = npc[0].GetComponent<NPCController>().flowchart;
        blockList = npc[0].GetComponent<NPCController>().flowchart.GetComponents<Block>();
    }

    public enum Direction
    {
        UP, DOWN, RIGHT, LEFT
    }

    // Update is called once per frame
    void Update()
    {
        if (!flowchart.HasExecutingBlocks())
        {
            move();
            timeFlow = true;
        }

        if (flowchart.HasExecutingBlocks())
        {
            talk();
            timeFlow = false;
        }

        animate();        
    }

    private void move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        rigidbody.velocity = new Vector2(moveX * speed, moveY * speed);

        if (rigidbody.velocity.y > 0 && rigidbody.velocity.y > Mathf.Abs(rigidbody.velocity.x))
        {
            direction = Direction.UP;
        }
        else if (rigidbody.velocity.y < 0 && Mathf.Abs(rigidbody.velocity.y) > Mathf.Abs(rigidbody.velocity.x))
        {
            direction = Direction.DOWN;
        }
        else if (rigidbody.velocity.x > 0 && rigidbody.velocity.x > Mathf.Abs(rigidbody.velocity.y))
        {
            direction = Direction.RIGHT;
        }
        else if (rigidbody.velocity.x < 0 && Mathf.Abs(rigidbody.velocity.x) > Mathf.Abs(rigidbody.velocity.y))
        {
            direction = Direction.LEFT;
        }
    }

    private void talk()
    {        
        if (Input.GetKey(KeyCode.E))
        {
            if (flowchart.HasExecutingBlocks())
            {
                Block block = blockList[blockIndex];
                //block.CommandList.
            }
            flowchart.ExecuteBlock(blockList[blockIndex], index);
            if (index <= blockList[blockIndex].CommandList.Count)
            {
                index += 1;
            }
            else
            {
                index = 1;        
            }            
        }
        blockIndex = flowchart.GetIntegerVariable("Index");
    }

    void animate()
    {
        switch (direction)
        {
            case (Direction.UP):
                renderer.sprite = sprites[1];
                break;
            case (Direction.DOWN):
                renderer.sprite = sprites[0];
                break;
            case (Direction.RIGHT):
                renderer.sprite = sprites[2];
                renderer.flipX = false;
                break;
            case (Direction.LEFT):
                renderer.sprite = sprites[2];
                renderer.flipX = true;
                break;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" && collision.gameObject.GetComponent<NPCController>().talkable)
        {
            DebugLog.print("collision");
            talk();
        }
    }

    public void SetPosition(Transform transform)
    {
        gameObject.transform.position = transform.position;
    }

    public void SetSpeaking(bool speak)
    {
        speaking = speak;
    }
}
