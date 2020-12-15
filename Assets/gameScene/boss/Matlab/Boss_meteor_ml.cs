using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss_meteor_ml : StateMachineBehaviour
{
    public GameObject meteorPrefab;
    public float t;
    public float g;
    public float minX;
    public float maxX;
    public float H;
    public float interval;
    
    GameObject boss;
    private float vx;
    private float vy;
    private float i;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.FindGameObjectWithTag("boss");
        i = minX;

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (i < maxX)
        {
            GameObject b = Instantiate(meteorPrefab) as GameObject;
            b.transform.position = boss.transform.position;
            b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            vx = (i - boss.transform.position.x) / t;
            vy = (H- boss.transform.position.y) / t + g / 2 * t;
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(vx, vy);
            i += interval;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        i = 0;
        animator.SetBool("meteorAttack",false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
