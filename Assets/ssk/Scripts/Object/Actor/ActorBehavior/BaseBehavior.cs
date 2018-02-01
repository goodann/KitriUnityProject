using System.Collections;
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
public abstract class BaseBehavior:MyBaseObejct
{
    //public 


    //protected
    
    protected Actor targetObject;
    //protected Animator CompAnimator;
    protected BaseAnimation ani;
    protected bool isAttacking;
    protected float ComboTimer;
    protected NextAttack nextAttack;
    protected bool isJumping;
    //property
    public virtual BaseAnimation Ani { get { return ani; } }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
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

        isAttacking = false;
        foreach (var i in targetObject.ListAttackColliders)
        {
            i.GetComponent<AttackCollider>().EndAttack();
            i.enabled = false;
        }
        if (nextAttack!=null)
        {
            if (nextAttack.isHand)
                AttackA();
            else
                AttackB();
            
        }


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
