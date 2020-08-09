using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEmoteTimer : StateMachineBehaviour
{
    public int numOptions;
    public float minIdleTime = 3;
    public float maxIdleTime = 7;

    private float waitTimer;
    private bool idling;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("option", UnityEngine.Random.Range(0, numOptions));
        waitTimer = UnityEngine.Random.Range(minIdleTime, maxIdleTime);
        idling = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitTimer -= Time.deltaTime;
        if (idling && waitTimer <= 0)
        {
            animator.SetTrigger("emote");
            idling = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
