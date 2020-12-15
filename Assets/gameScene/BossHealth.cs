using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health;
    public Animator animator;

    //Reduces health of player by one when collision the bullet
    //Makes player animator boolean dead true if health is gone
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            health--;
        }

        if (health < 0)
        {
            animator.SetBool("die",true);
        }
    }


}
