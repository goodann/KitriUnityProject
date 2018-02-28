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
    protected bool isMoving;
    protected int comboCount;
    protected int attackCombo;
    public int AttackCombo { get { return attackCombo; } }
    
    //property
    public int ComboCount { get { return comboCount; } }
    public virtual BaseAnimation Ani { get { return ani; } }
    public bool IsAnimationPlaing { get { return isAnimationPlaing; } set { isAnimationPlaing = value; } }
    public bool IsJumping { get { return isJumping; } }
    public bool IsMoving { get {return isMoving; } }
    protected bool isDownnig = false;
    
    public void ComboAdd()
    {
        attackCombo++;
    }
    public virtual void ComboInit()
    {
        //콤보초기화
        if (attackCombo > targetObject.MaxCombo)
            targetObject.MaxCombo = attackCombo;
        attackCombo = 0;
        comboCount = 0;
        ComboTimer = 0.0f;
        GameObject.Find("ComboText").SendMessage("SetComboOffAnim");
        GameObject.Find("ComboText").SendMessage("SetCombo", 0f);
    }
    public virtual void Init(Actor target, Animator animator,string name)
    {
        targetObject = target;
        //CompAnimator = animator;
        EndAttack();
    }
    public void AttackColliderEnable(EAttackColliderIndex index)
    {
        if (index == EAttackColliderIndex.ACI_Weapon)
        {
            targetObject.Weapon.enabled = true;
        }
        else
        {
            targetObject.ListAttackColliders[(int)index].enabled = true;
        }
    }
    public void AniPlayStart()
    {
        isAnimationPlaing = true;
    }
    public virtual void Move()
    {
        ani.AniMove();
        isMoving = true;
    }
    public virtual void Rolling()
    {
        ani.AniRolling();
         
    }
    public abstract void Skill(int charged);
    public virtual void Damaged(int damage)
    {
        ani.AniDamaged();
    }

    public virtual void Dead()
    {
        ani.AniDead();
    }
    public abstract void AttackA();
    public abstract void AttackB();
    public virtual  void onDamage(int damage)
    {
        
        ani.AniDamaged();
    }
    public virtual void Stop()
    {
        ani.AniStop();
        isMoving = false;
        isJumping = false;
        isDownnig = false;
    }

    protected virtual void Update()
    {
        if (isJumping)
        {
            //print(targetObject.Velocity);

            if (isDownnig && targetObject.IsGrounded)
            {
                Stop();
                ani.AniJumpEnd();
                isJumping = false;
                isDownnig = false;

            }
            else if (targetObject.Velocity.y < -0.1f)
            {
                isDownnig = true;
            }
        }
        if (targetObject.IsRolling)
        {
            //targetObject.Move(transform.forward);
        }
        ComboTimer += Time.deltaTime;
    }
    public void EndAttackCollider()
    {
        for (int i = 0; i < targetObject.ListAttackCollidersComp.Count; ++i)
        {
            targetObject.EndAttack();
            targetObject.ListAttackColliders[i].enabled = false;

        }
        if(targetObject.Weapon!=null)
            targetObject.Weapon.enabled = false;
        
    }
    public void EndAttack()
    {
        print("end Attack by BaseBehavior");
        isAnimationPlaing = false;
        EndAttackCollider();
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
    public virtual void switchEq(EEquipmentState NowEq, RuntimeAnimatorController NowAniCon)
    {
        ani.AniSwitchEq(NowAniCon);
    }

}
