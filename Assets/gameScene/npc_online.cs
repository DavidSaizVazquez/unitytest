using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_online : MonoBehaviour
{
    public float runSpeed = 40f;
    public GameObject ghost;
    public Animator animator;
    public CharacterController2D controller;
    private bool m_FacingRight = true;  
    private bool dead = false;

    // RECIEVED DATA:
    private string name;
    private float x_pos;
    private float y_pos;
    private int state;
    private int direction;
    
    public void setName(string name)
    {
        this.name = name;
    }

    void Start(){
        ghost.GetComponent<Renderer>().enabled=false;
    }

    //Given a position from server moves npc to that position
    //Animation of npc is updated from state and direction
    //If player is dead, puts ghost
    // Update is called once per frame
    public void updateCharacter(float x_pos, float y_pos, int state, float direction)
    
    {
        updateAnimatorState(state, direction);

        transform.position =
            new Vector3(x_pos, y_pos, transform.position.z);
        

        if (!dead && state==1)
        {
            dead = true;
            ghost.GetComponent<Renderer>().enabled = true;
            ghost.GetComponent<Transform>().position =
                new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 v = ghost.GetComponent<Rigidbody2D>().velocity;
            v.y = 3f;
            ghost.GetComponent<Rigidbody2D>().velocity = v;
        }
        
    }

    
    //updates animation seen in npc according to state code
    public void updateAnimatorState(int state, float direction)
    {
        animator.SetBool("Crouch", state==0);
        animator.SetBool("Dead", state==1);
        animator.SetBool("Roll", state==2);
        animator.SetBool("Jumping", state==3);
        animator.SetBool("Running", state==4);
        animator.SetBool("Dash", state==5);

        
        
        // If the input is moving the player right and the player is facing left...
        if (direction > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (direction < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }
    
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    



}
