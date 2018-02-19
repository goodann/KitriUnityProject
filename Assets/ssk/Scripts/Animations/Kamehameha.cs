using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamehameha : AniStateAttack
{
    static GameObject beamPrefab;
    GameObject instBeam;

    float timer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SendMessage("AniPlayStart");
        if(beamPrefab==null)
            beamPrefab = Resources.Load("ssk/prefabs/GeroBeam1") as GameObject;
        
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        StageManager.mainPlayer.Behavior.IsAnimationPlaing = false;
        timer += Time.deltaTime;
        if(timer>=0.3f && instBeam == null)
        {
            

            animator.SendMessage("AniPlayStart");
            //instBeam = Instantiate(beamPrefab, animator.transform.position + beamPosition, animator.transform.rotation);
            instBeam = Instantiate(beamPrefab,StageManager.mainPlayer.transform);
            //instBeam.transform.position += animator.transform.up*0.5f+ animator.transform.forward * 0.5f;
            
        }
        if (timer >= 5.0f)
        {
            instBeam.GetComponent<BeamParam>().bEnd = true;
            
        }
        if (timer >= 6.5f)
        {
            StageManager.mainPlayer.NowMoveSpeed = StageManager.mainPlayer.MoveSpeed;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
