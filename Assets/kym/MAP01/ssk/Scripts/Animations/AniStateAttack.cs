using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniStateAttack : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    Player CompPlayer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CompPlayer = animator.transform.GetComponent<Player>();
        CompPlayer.Behavior.IsAnimationPlaing = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CompPlayer == null)
        {
            CompPlayer = animator.transform.GetComponent<Player>();
        }
        //CompPlayer.IsAttacking = true;
        //if (stateInfo.normalizedTime > 1.0f && CompPlayer.Behavior.IsAnimationPlaing)
        //{
            
        //    CompPlayer.Behavior.EndAttack();
            
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CompPlayer.Behavior.IsAnimationPlaing = false;
        CompPlayer.Behavior.EndAttack();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("OnStateMove : "+ animator.GetCurrentAnimatorStateInfo(0));
        //Debug.Log(name + ":" + (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash));
    }

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
