using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateState : StateMachineBehaviour
{
    public int value;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //Sets the players state to an int that represents the animation its currently doing
    //Used to update the player animations in the games of other players where they will be an npc
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        globalData.State = value;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
