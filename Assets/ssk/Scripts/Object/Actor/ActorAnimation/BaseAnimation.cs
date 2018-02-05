using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//애니메이션에 관한 것을 처리하는 클래스
public class BaseAnimation:MyBaseObejct  {
    protected BaseBehavior targetBehavior;
    protected Actor targetObject;
    protected Animator CompAnimator;
    
    public virtual void AnimatorInit(Actor target ,BaseBehavior combat, Animator animator)
    {
        targetBehavior = combat;
        targetObject = target;
        CompAnimator = animator;
    }

    public virtual void AniJump()
    {
        print("AniJump!!");
        CompAnimator.SetBool("Jump", true);

    }
    public virtual void AniJumpEnd()
    {
        print("AniJumpEnd!!");
        CompAnimator.SetBool("Jump", false);
    }
    public virtual void AniAttackA()
    {

    }
    public virtual void AniAttackB()
    {

    }
    public virtual void AniMove()
    {
        CompAnimator.SetBool("Moving", true);
    }
    public virtual void AniRolling()
    {
        CompAnimator.SetTrigger("Rolling");
    }
    public virtual void AniDead()
    {
        CompAnimator.SetTrigger("Dying");
    }
    public virtual void AniSkill(int charged)
    {

    }
    public virtual void AniDamaged()
    {

    }
    public virtual void AniStop()
    {

    }
    public virtual void AniSwitchEq(RuntimeAnimatorController newAnimatior)
    {
        CompAnimator.runtimeAnimatorController = newAnimatior;
    }

}
