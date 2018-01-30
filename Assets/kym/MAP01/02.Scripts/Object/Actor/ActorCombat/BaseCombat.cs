using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombat : MyBaseObejct
{
    protected Actor targetObject;

    public BaseAnimation CompAnimation;
    protected Animator CompAnimator;

    public virtual BaseAnimation Animatoion
    {
        get { return CompAnimation; }
    }
    protected bool isAttacking;

    public virtual void Init(Actor target,Animator animator)
    {
        targetObject = target;
        CompAnimator = animator;
        EndAttack();
    }
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    protected float ComboTimer;
    protected virtual void Update()
    {
        ComboTimer += Time.deltaTime;
    }
    public virtual void Attack1()
    {
        
    }
    public virtual void Attack2()
    {

    }
    public virtual void Attack3()
    {

    }
    public virtual void Attack4()
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

    }

}
