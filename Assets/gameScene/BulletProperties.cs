using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    public string tag;
    public float rotation;
    
    //Makes bullet go through boss without crashing 
    //Destroys bullet on touch of certain layers defined in the config matrix of the project
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "boss") Physics2D.IgnoreLayerCollision(13, 12);
        if (tag == "Player") Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(13, 10);
        if (!(collision.gameObject.CompareTag(tag) || collision.gameObject.CompareTag("bossBullet") || collision.gameObject.CompareTag("bullet")))
        { 
            Destroy(gameObject);
        }
    }
    //Destroys bulletif it goes out of camera
    //Makes bullet point to direction its going in
    private void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
        }

        Vector2 v = gameObject.GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+rotation, Vector3.forward);
    }
}
