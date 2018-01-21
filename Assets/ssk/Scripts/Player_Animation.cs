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
        if (isGrounded)
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
        bool isJumping = CompAnimator.GetBool("Jump");
        if (isJumping)
        {
            if (isGrounded && moveDirection.y < 0.2f)
            {
                CompAnimator.SetBool("Jump", false);
            }
        }
        isJumpKicking = CompAnimator.GetBool("JumpKick");
        if (isJumpKicking)
        {
            if (isGrounded)
            {
                CompAnimator.SetBool("JumpKick", false);
                CompAnimator.SetBool("Jump", false);
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

    void AniAttack(bool isHand)
    {
        CompAnimator.SetBool("Moving", false);
        if (isLeft)
        {
            //애니메이션L
            if (isHand)
            {
                //애니메이션LP
                CompAnimator.SetTrigger("LeftPunch");
            }
            else
            {
                //애니메이션LK
                CompAnimator.SetTrigger("LeftKick");
            }
        }
        else
        {
            //애니메이션R
            if (isHand)
            {
                //애니메이션RP
                CompAnimator.SetTrigger("RightPunch");
            }
            else
            {
                //애니메이션RK
                CompAnimator.SetTrigger("RightKick");
            }
        }
    }
    void AniJump()
    {
        CompAnimator.SetBool("Jump", true);
        
    }
    void AniJumpKick()
    {
        
        CompAnimator.SetBool("JumpKick", true);
        //CompAnimator.SetBool("Jump", false);


    }
    void AniSwitchEq()
    {
        CompAnimator.runtimeAnimatorController= CompAnimators[(int)NowEq]; 
    }
}
