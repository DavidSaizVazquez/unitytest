using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot_ml : StateMachineBehaviour
{
    
    
    private Animator animator;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    GameObject player;
    GameObject boss;
    Vector2 diff;
    Vector2 direction;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("boss");
        diff = new Vector2(player.transform.position.x-boss.transform.position.x, player.transform.position.y - boss.transform.position.y);
        float distance = diff.magnitude;
        direction = diff / distance;
        direction.Normalize();
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        bulletPrefab.layer = 13;
        b.transform.position = boss.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("shootBullet",false);
    }
    
}
