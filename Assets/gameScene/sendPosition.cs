using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendPosition : MonoBehaviour
{

    NoGameFoundClient.ServerConnection server;

    public Animator animator;
    public float refreshPos = 0.03f;

    private float counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        server = NoGameFoundClient.ServerConnection.getInstance();
    }

    // Update is called once per frame
    //Sends the players position (x,y), state which depends on animation its doing, and number to know where the player is facing
    //Sent to server to send to other users playing in the same game to update position of their npcs
    void FixedUpdate()
    {
        counter += Time.deltaTime;
        if (counter > refreshPos)
        {
            // 20/x,y,state
            string message = "20/";
            message += gameObject.transform.position.x;
            message += "/";
            message += gameObject.transform.position.y;
            message += "/";
            message += globalData.State.ToString();
            message += "/";
            message += animator.GetFloat("Horizontal");
            message += "~";
            server.SendMessage(message);
            counter = 0;
        }


    }
}
