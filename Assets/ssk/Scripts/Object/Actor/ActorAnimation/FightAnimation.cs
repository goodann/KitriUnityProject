using System.Collections;
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
        base.AniMove();
        //CompAnimator.SetBool("Moving", true);
    }
    public override void AniJump()
    {
        base.AniJump();
        //CompAnimator.SetBool("Jump", true);
    }
    //public override void AniUpdate()
    //{
    //    if (targetObject.IsGrounded && !targetBehavior.IsAnimationPlaing)
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
    public void AniJumpPunch()
    {
        
        CompAnimator.SetBool("JumpPunch", true);
        CompAnimator.Play("JumpPunch");
        print("점프펀치");
    }
    public void AniJumpKick()
    {

        //print("AniJumpKick()");
        CompAnimator.SetBool("JumpKick", true);
        //CompAnimator.SetBool("Jump", false);


    }
    public override void AniStop()
    {
        print("AniStop!!");
    
        CompAnimator.SetBool("JumpKick", false);
        CompAnimator.SetBool("JumpPunch", false);
        base.AniStop();
    }

    public override void AniSkill(int charged)
    {
        AniStop();
        if (charged >= 100 && charged<200)
        {
            CompAnimator.SetTrigger("Skill1");
            targetObject.IsUpperAttack = true;
            
        }
        else if (charged >= 200 && charged<300)
        {
            CompAnimator.SetTrigger("Skill2");
        }
        else if (charged >= 300)
        {
            CompAnimator.SetTrigger("Skill3");
        }
    }
    public override void AniDamaged()
    {
        
        base.AniDamaged();
        CompAnimator.SetTrigger("Hit1");
    }
}
