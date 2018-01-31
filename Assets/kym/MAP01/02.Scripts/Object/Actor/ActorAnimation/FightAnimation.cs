﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAnimation : BaseAnimation {
    //new protected FightBehavior targetBehavior;

    public void AnimatorInit(Actor target, FightBehavior combat, Animator animator)
    {
        base.AnimatorInit(target, combat, animator);
        targetBehavior = combat;
        targetObject = target;
        CompAnimator = animator;
    }
    public override void AniMove()
    {
        CompAnimator.SetBool("Moving", true);
    }
    public override void AniJump()
    {
        CompAnimator.SetBool("Jump", true);
    }
    //public override void AniUpdate()
    //{
    //    if (targetObject.IsGrounded && !targetBehavior.IsAttacking)
    //    {
    //        if (targetObject.SqrtVel > 0.001f)
    //        {
    //            CompAnimator.SetBool("Moving", true);
    //        }
    //        else
    //        {
    //            CompAnimator.SetBool("Moving", false);
    //        }
    //    }
    //    else
    //    {
    //        CompAnimator.SetBool("Moving", false);
    //    }
    //    bool isJumping = CompAnimator.GetBool("Jump");
    //    if (isJumping)
    //    {
    //        if (targetObject.IsGrounded && targetObject.Velocity.y < 0.2f)
    //        {
    //            CompAnimator.SetBool("Jump", false);
    //        }
    //    }
    //    targetBehavior.IsJumpKicking = CompAnimator.GetBool("JumpKick");
    //    if (targetBehavior.IsJumpKicking)
    //    {
    //        if (targetObject.IsGrounded)
    //        {
    //            CompAnimator.SetBool("JumpKick", false);
    //            CompAnimator.SetBool("Jump", false);
    //            targetBehavior.jumpKickEnd();
    //        }
    //    }
    //    if (targetBehavior.IsJumpKickDowning)
    //    {
    //        targetBehavior.jumpKicking();

    //    }
    //}

    public void AniAttack(bool isHand)
    {
        CompAnimator.SetBool("Moving", false);
        if ((targetBehavior as FightBehavior ).NextAttackIsLeft)
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
    
    public void AniFlipKick()
    {
        CompAnimator.SetTrigger("FlipKick");
    }
    public void AniSpacialKick()
    {
        CompAnimator.SetTrigger("SpacialKick");
    }
    public void AniJumpKick()
    {

        //print("AniJumpKick()");
        CompAnimator.SetBool("JumpKick", true);
        //CompAnimator.SetBool("Jump", false);


    }
    public override void AniStop()
    {
        CompAnimator.SetBool("Moving", false);
        CompAnimator.SetBool("Jump", false);
        CompAnimator.SetBool("JumpKick", false);
        base.AniStop();
    }
}
