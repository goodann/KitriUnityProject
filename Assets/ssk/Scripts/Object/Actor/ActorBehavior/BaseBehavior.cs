﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NextAttack
{
    public bool isHand;
    public NextAttack(bool ishand)
    {
        isHand = ishand;
    }
    NextAttack()
    {
        isHand = false;
    }
}
//애니메이션을 가지고있는 행동 클래스
//공격 판정Collider관리
public abstract class BaseBehavior:MyBaseObejct
{
    //public 


    //protected
    
    protected Actor targetObject;
    //protected Animator CompAnimator;
    protected BaseAnimation ani;
    protected bool isAnimationPlaing;
    protected float ComboTimer;
    protected NextAttack nextAttack;
    protected bool isJumping;
    //property
    public virtual BaseAnimation Ani { get { return ani; } }
    public bool IsAnimationPlaing { get { return isAnimationPlaing; } set { isAnimationPlaing = value; } }
    public bool IsJumping { get { return isJumping; } }

    public virtual void Init(Actor target, Animator animator)
    {
        targetObject = target;
        //CompAnimator = animator;
        EndAttack();
    }


    public virtual void Move()
    {
        ani.AniMove();
    }
    public abstract void Skill(int charged);
    public abstract void Damaged(int damage);
    public abstract void Dead();
    public abstract void AttackA();
    public abstract void AttackB();
    public virtual  void onDamaged(int damage)
    {
        ani.AniDamaged();
    }
    public virtual void Stop()
    {
        ani.AniStop();
    }

    protected virtual void Update()
    {
        ComboTimer += Time.deltaTime;
    }
    public void EndAttack()
    {

        isAnimationPlaing = false;
        for(int i=0; i<targetObject.ListAttackCollidersComp.Count;++i)
        {
            targetObject.EndAttack();
            targetObject.ListAttackColliders[i].enabled = false;
            
        }

        if (nextAttack!=null)
        {
            if (nextAttack.isHand)
                AttackA();
            else
                AttackB();
            
        }
        targetObject.AttackDirction = transform.forward*0.1f;


    }
    public virtual void Jump()
    {
        isJumping = true;
        ani.AniStop();
        ani.AniJump();
    }
    public virtual void switchEq(EEquipmentState NowEq)
    {
        ani.AniSwitchEq( (targetObject as Player).CompAnimators[(int)NowEq]);
    }

}
