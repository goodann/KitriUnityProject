using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCombat : MyBaseObejct
{
    //public 
    public BaseAnimation CompAnimation;

    //protected
    protected Actor targetObject;
    protected Animator CompAnimator;
    protected bool isAttacking;

    protected float ComboTimer;


    //property
    public virtual BaseAnimation Animatoion{get { return CompAnimation; }}
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }

    public virtual void Init(Actor target,Animator animator)
    {
        targetObject = target;
        CompAnimator = animator;
        EndAttack();
    }
    
    
    protected virtual void Update()
    {
        ComboTimer += Time.deltaTime;
    }
    public abstract void AttackA();
    public abstract void AttackB();

    public virtual void Skill(int charged)
    {

    }

    public void EndAttack()
    {
        isAttacking = false;
        foreach (var i in targetObject.ListAttackColliders)
        {
            i.enabled = false;
        }

    }

    public virtual void Jump()
    {
        CompAnimation.AniJump();
    }

}
