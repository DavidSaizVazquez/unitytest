using UnityEngine;

namespace NoGameFoundClient.gameScene
{
    public class npc_local : MonoBehaviour
    {
     private string name;
    public float runSpeed = 40f;
    public GameObject ghost;
    public Animator animator;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool dead = false;
    public Camera camera;
    public CharacterController2D controller;

    // RECIEVED DATA:
    bool jumpKey = false;
    bool releaseJumpKey=false;
    bool crouckKey = false;
    bool releaseCrouchKey = false;
    bool killKey = false;
    //can be -1 0 or 1
    float horizontalKey = (float) 0.0;

    public void setName(string name)
    {
        this.name = name;
    }

    void Start(){
        ghost.GetComponent<Renderer>().enabled=false;
    }

    // Detects keys clicked by user (PLAYER 2) WASD keys and updates animator states which updates visible animation onscreen
    // Update is called once per frame
    public void updateCharacter(bool jumpKey,bool releaseJumpKey,bool crouckKey,bool releaseCrouchKey,bool killKey,float horizontalKey)
    {
        horizontalMove = horizontalKey * runSpeed;
        animator.SetFloat("Horizontal",horizontalKey);

        if (jumpKey)
        {
            animator.SetBool("Jumping",true);
            jump = true;
        } else if (releaseJumpKey)
        {
            animator.SetBool("Jumping",false);
        }

        if (crouckKey)
        {
            animator.SetBool("Crouch",true);
            crouch = true;
        }else if (releaseCrouchKey)
        {
            crouch = false;
            animator.SetBool("Crouch",false);
        }

        //Kills character and displays flying ghost from corpse
        if(killKey && !dead){
            dead=true;
            animator.SetBool("Dead",true);
            ghost.GetComponent<Renderer>().enabled=true;            
            ghost.GetComponent<Transform>().position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            Vector3 v = ghost.GetComponent<Rigidbody2D>().velocity;
            v.y=3f;
            ghost.GetComponent<Rigidbody2D>().velocity= v;
        }
    }

    //Moves character with controller
    //Removes ghost if it leavescamera bounds
    void FixedUpdate()
    {

        if(!dead){
            controller.Move(horizontalMove * Time.fixedDeltaTime,crouch,jump,false, animator);
        } else {
            Vector3 screenPosition = camera.WorldToScreenPoint(ghost.GetComponent<Transform>().position);
            if (screenPosition.y > Screen.height || screenPosition.y < 0)
            {
                    ghost.GetComponent<Renderer>().enabled=false;
            }
        }
        jump = false;
        
        
    }

        
    }
}