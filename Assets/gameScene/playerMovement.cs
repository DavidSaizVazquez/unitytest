﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public GameObject ghost;
    public Camera camera;
    public HealthHelp health;
    public GameObject dashObject;
    public Animator windAnimator;
    public float dashDelay;
    
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool dead = false;
    bool dash = false;
    private float clock;

    void Start(){
        ghost.GetComponent<Renderer>().enabled=false;
        clock = dashDelay;
    }

    // Detects keys clicked by user (PLAYER 1) up and down keys and updates animator states which updates visible animation onscreen
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Horizontal",Input.GetAxisRaw("Horizontal"));
        clock += Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jumping",true);
            jump = true;
        } else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("Jumping",false);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("Crouch",true);
            crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("Crouch",false);
        }
        
        if (Input.GetButtonDown("Dash"))
        {
            if (clock > dashDelay)
            {
                animator.SetBool("Dash", true);
                dash = true;
                dashObject.GetComponent<Transform>().position =
                    new Vector3(transform.position.x, transform.position.y, transform.position.z);

                windAnimator.SetBool("Dash", true);
                clock = 0;
            }
            else
            {
                dash = false;
            }

        }else if (Input.GetButtonUp("Dash"))
        {
            dash = false;
            animator.SetBool("Dash",false);
            windAnimator.SetBool("Dash",false);

        }
        
        //Kills character and displays flying ghost from corpse
        if((Input.GetButtonDown("Kill") || health.getHP() < 1) && !dead){
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
        if(!animator.GetBool("Dead")){
            controller.Move(horizontalMove * Time.fixedDeltaTime,crouch,jump,dash ,animator);
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

