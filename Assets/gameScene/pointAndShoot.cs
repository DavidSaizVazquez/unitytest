using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAndShoot : MonoBehaviour
{
    public GameObject cross;
    public GameObject player;
    public GameObject bulletPrefab;

    public float bulletSpeed = 60.0f;
    public float bulletBuffer = 0.7f;
        
    private Vector2 target;
    private Vector2 diff;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    //Moves user cursor to shoot and shoots if click is detected
    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        cross.transform.position = new Vector2(target.x,target.y);
        diff = new Vector2( target.x - player.transform.position.x, target.y - player.transform.position.y);
        if (Input.GetMouseButtonDown(0))
        {
            float distance = diff.magnitude;
            direction = diff / distance;
            direction.Normalize();
            fireBullet(direction, 0);
        }
    }

    //Creates bullet that goes from player to user pointer and given velocity
    void fireBullet(Vector3 dir, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = player.transform.position+dir*bulletBuffer;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
        b.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
    }
}
