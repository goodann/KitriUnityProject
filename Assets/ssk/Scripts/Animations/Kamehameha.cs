using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamehameha : StateMachineBehaviour {
    static GameObject beamPrefab;
    GameObject instBeam;
    Vector3 beamPosition = new Vector3(0, 0.5f, 0.5f);
    float timer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(beamPrefab==null)
            beamPrefab = Resources.Load("ssk/prefabs/GeroBeam1") as GameObject;
        
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer>=0.3f && instBeam == null)
        {
            //instBeam = Instantiate(beamPrefab, animator.transform.position + beamPosition, animator.transform.rotation);
            Vector3 pos = animator.transform.position + animator.transform.forward*0.5f;
            instBeam = Instantiate(beamPrefab, pos, animator.transform.rotation);
            instBeam.transform.position += animator.transform.up*0.5f;
            
        }
        if (timer >= 5f)
        {
            Destroy(instBeam);
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
