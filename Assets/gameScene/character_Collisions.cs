using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class character_Collisions : MonoBehaviour
{
    public HealthHelp hpbar;
    public float invSeconds = 3;
    private float time;
    private float lastTime;
    
    //if player collisions with bullet or boss its health is reduced
    private void OnCollisionEnter2D(Collision2D collision)
    {
        time += Time.time-lastTime;
        lastTime = Time.time;
        if (time > invSeconds)
        {
            if (collision.gameObject.tag == "bossBullet" || collision.gameObject.tag == "boss")
            {
                hpbar.reduceHealth();
                time = 0;
            }
           
        }
    }
}
