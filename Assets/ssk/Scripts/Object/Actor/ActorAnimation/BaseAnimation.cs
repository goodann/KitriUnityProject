using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimation : MyBaseObejct {
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
        CompAnimator.SetBool("Jump", true);

    }
    public virtual void AniJumpEnd()
    {
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

    }
    public virtual void AniDead()
    {

    }
    public virtual void AniSkill()
    {

    }
    public virtual void AniDamaged()
    {

    }
    public virtual void AniStop()
    {

    }

}
