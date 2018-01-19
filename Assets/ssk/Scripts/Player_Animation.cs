using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    PsStay = 0,
    PsMoving = 1,
    PsAttack=2,
}
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public partial class Player : MonoBehaviour {

    Quaternion jumpKickQua;
    Quaternion jumpKickQuaOrign;
    void AnimatorInit()
    {
        CompAnimator = GetComponent<Animator>();
    }
    void AniUpdate()
    {
        if (CompCharCon.isGrounded)
        {
            if(sqrtVel > 0.001f)
            {
                CompAnimator.SetBool("Moving", true);
            }
            else
            {
                CompAnimator.SetBool("Moving", false);
            }
        }
        else
        {
                CompAnimator.SetBool("Moving", false);
        }


        if (CompAnimator.GetBool("Jump"))
        {
            if(CompCharCon.isGrounded)
                CompAnimator.SetBool("Jump", false);
        }
        isJumpKicking = CompAnimator.GetBool("JumpKick");
        if (isJumpKicking)
        {
            if (CompCharCon.isGrounded)
            {
                CompAnimator.SetBool("JumpKick", false);
                isJumpKicking = false;
                isJumpKickDowning = false;
                transform.rotation = jumpKickQuaOrign;
            }
        }
        if (isJumpKickDowning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, jumpKickQua, Time.deltaTime*5);

        }
    }
    void AniJump()
    {
        CompAnimator.SetBool("Jump", true);
        
    }
    void AniJumpKick()
    {
        
        CompAnimator.SetBool("JumpKick", true);
        CompAnimator.SetBool("Jump", false);


    }
    void AniSwitchEq()
    {
        CompAnimator.runtimeAnimatorController= CompAnimators[(int)NowEq]; 
    }
}
