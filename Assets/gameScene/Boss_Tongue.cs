using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Tongue : StateMachineBehaviour
{
    private Animator animator;
    GameObject boss;

    Vector2 diff;
    Vector2 direction;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //makes tongue visible when shooting the long tongue
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.FindGameObjectWithTag("boss");
        boss.transform.Find("TongueHit").transform.Find("toungeEnd").GetComponent<Renderer>().enabled = true;
        boss.transform.Find("TongueHit").transform.Find("toungeLong").GetComponent<Renderer>().enabled = true;
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //Hides tongue when its done shooting
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.transform.Find("TongueHit").transform.Find("toungeEnd").GetComponent<Renderer>().enabled=false;            
        boss.transform.Find("TongueHit").transform.Find("toungeLong").GetComponent<Renderer>().enabled=false;  
        animator.SetBool("shootTongue",false);
    }
}

