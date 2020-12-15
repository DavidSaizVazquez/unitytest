using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float InitialY;
    public float oscillation;
    public float bulletRate;
    public float countMax;
    private float counter=0;


    float t=0;

    Transform player;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player= GameObject.FindGameObjectWithTag("Player").transform;
        rb = GameObject.FindGameObjectWithTag("boss").GetComponent<Rigidbody2D>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t += Time.deltaTime;
        rb.transform.position = new Vector3(rb.transform.position.x, oscillation * Mathf.Sin(t) + InitialY, rb.transform.position.z);
        if (Random.Range(0, 100) > bulletRate)
        {
            if (counter < countMax && !animator.GetBool("shootTongue"))
            {
                animator.SetBool("shootBullet",true);
                counter+=Time.deltaTime;
            }
            else
            {
                animator.SetBool("shootTongue",true);
                counter = 0;
            }
        }
    }
    

}
