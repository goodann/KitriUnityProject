﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehavior : BaseBehavior {
    //new protected FightAnimation ani;
    //override public BaseAnimation Ani
    //{
    //    get
    //    {
    //        return ani;
    //    }
    //}
    protected bool isLeft;
    public bool NextAttackIsLeft
    {
        get { return isLeft; }
    }
    protected bool isJumpKicking;
    protected bool isJumpKickDowning;
    protected int comboCount;
    protected uint comboSignal;

    public bool IsJumpKicking
    {
        get { return isJumpKicking; }
        set { isJumpKicking = value; }
    }
    public bool IsJumpKickDowning
    {
        get { return isJumpKickDowning; }
        //set { isJumpKickDowning = value; }
    }

    Quaternion jumpKickQua;
    Quaternion jumpKickQuaOrign;
    
    public void jumpKickStart()
    {

    }
    public void jumpKickEnd()
    {

        isJumpKicking = false;
        isJumpKickDowning = false;
        transform.rotation = jumpKickQuaOrign;
    }
    public void jumpKicking()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, jumpKickQua, Time.deltaTime * 5);
    }


    public override void Init(Actor target, Animator animator)
    {
        ComboInit();
        isLeft = true;
        
        base.Init(target,animator);
        

        ani = gameObject.AddComponent<FightAnimation>();
        ani.AnimatorInit(target, this, animator);
        EndAttack();

    }
    public override void AttackA()
    {
        if (!IsJumping && !isAttacking)
        {
            nextAttack = null;
            Attack(true);
        }
    }
    public override void AttackB()
    {
        if (!isAttacking)
        {
            nextAttack = null;
            Attack(false);
        }
    }
    public override void Damaged(int damage)
    {
        
    }
    public override void Dead()
    {
        
    }

    public override void Skill(int charged)
    {

    }
    public override void Stop()
    {
        isJumpKickDowning = false;
        isJumpKicking = false;
        isJumping = false;
        isDownnig = false;
        ani.AniStop();
    }

    bool isDownnig = false;
    protected override void Update()
    {
        base.Update();
        if (isJumping)
        {
            //print(targetObject.Velocity);

            if (isDownnig && targetObject.IsGrounded)
            {
                ani.AniJumpEnd();
                //isJumping = false;
                //isDownnig = false;
                Stop();
            }
            if (targetObject.Velocity.y < -0.1f)
            {
                isDownnig = true;
            }
        }
        if (isJumpKicking)
        {
            targetObject.Move(Physics.gravity * 1.5f * Time.deltaTime);
            if (targetObject.IsGrounded)
            {
                Stop();
                
            }
        }
        if (isJumpKickDowning)
        {

            targetObject.Move(Physics.gravity * 1f * Time.deltaTime);
            Vector3 dir2 = transform.forward;
            dir2.y = 0;
            targetObject.Move(dir2 * Time.deltaTime * 100f);
            if (targetObject.IsGrounded)
            {
                
                Stop();
            }
        }
        if (ComboTimer > 2.0f)
        {
            ComboInit();
        }
    }
    public void ComboInit()
    {
        //콤보초기화
        comboSignal = 0;
        comboCount = 0;
        ComboTimer = 0.0f;
        isLeft = true;
    }
    public void JumpKick()
    {
        isJumpKickDowning = true;

        jumpKickQua = transform.rotation * Quaternion.Euler(30, 0, 0);
        jumpKickQuaOrign = transform.rotation;
        //Time.timeScale = 0.1f;
    }
    
    public void AttackColliderEnable(EAttackColliderIndex index)
    {
        targetObject.ListAttackColliders[(int)index].enabled = true;
    }


    public void Attack(bool isHand)
    {
        if (isAttacking && nextAttack == null)
        {
            nextAttack = new NextAttack (isHand);
        }
        else
        {
            isAttacking = true;

            if (!IsJumping)
            {
                targetObject.Velocity = Vector3.zero;
                //combo

                //print("signal : " + comboSignal.ToString("x") + " counCount = " + comboCount + "isleft"+ isLeft);
                //combo 저장
                comboSignal = comboSignal << 1;
                comboSignal += (uint)(isHand ? 1 : 0);

                ComboTimer = 0;
                comboCount++;
                if (comboCount == 6 && comboSignal == 0x3C)
                {
                    //ani.AniFlipKick();
                    ani.SendMessage("AniFlipKick");
                    AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                    AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);

                }
                else if (comboCount == 7 && comboSignal == 0x78)
                {
                    //print(ComboCount + ", " + ComboSignal.ToString("x"));
                    //spacialKick
                    AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                    AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);
                    //ani.AniSpacialKick();
                    ani.SendMessage("AniSpacialKick");
                    ComboInit();
                }
                else
                {
                    if (isLeft)
                    {
                        //애니메이션L
                        if (isHand)
                        {

                            //애니메이션LH
                            AttackColliderEnable(EAttackColliderIndex.ACI_LeftHand);
                        }
                        else
                        {
                            //애니메이션LF
                            AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                        }
                    }
                    else
                    {
                        //애니메이션R
                        if (isHand)
                        {
                            //애니메이션RH
                            AttackColliderEnable(EAttackColliderIndex.ACI_RightHand);
                        }
                        else
                        {
                            //애니메이션RF
                            AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);
                        }
                    }
                    isLeft = !isLeft;
                    //ani.AniAttack(isHand);
                    ani.SendMessage("AniAttack",isHand);

                    //손 번갈아공격
                    

                }
            }
            else
            {
                //공중공격
                if (isHand)
                {
                    //점프손
                }
                else
                {
                    targetObject.Move(Vector3.up * targetObject.JumpForce * 0.05f + Vector3.forward );
                    //ani.AniJumpKick();
                    ani.SendMessage("AniJumpKick");
                    //점프킥


                }
            }
        }
    }

    //public override void Jump()
    //{
    //    ani.AniJump();
    //}


}