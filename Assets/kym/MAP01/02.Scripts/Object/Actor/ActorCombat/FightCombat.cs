using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCombat : BaseCombat {
    new protected FightAnimation CompAnimation;
    override public BaseAnimation Animatoion
    {
        get
        {
            return CompAnimation;
        }
    }
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
        

        CompAnimation = gameObject.AddComponent<FightAnimation>();
        CompAnimation.AnimatorInit(target, this, animator);
        EndAttack();

    }
    public override void Attack1()
    {
        Attack(true);
        base.Attack1();
    }
    public override void Attack2()
    {
        Attack(false);
        base.Attack2();
    }

    protected override void Update()
    {
        base.Update();
        if (isJumpKicking)
        {
            targetObject.Move(Physics.gravity * 1.5f * Time.deltaTime);
        }
        if (isJumpKickDowning)
        {

            targetObject.Move(Physics.gravity * 1f * Time.deltaTime);
            Vector3 dir2 = transform.forward;
            dir2.y = 0;
            targetObject.Move(dir2 * Time.deltaTime * 300f);
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
        if (isAttacking)
        {

        }
        else
        {
            isAttacking = true;

            if (targetObject.IsGrounded)
            {
                targetObject.Velocity = Vector3.zero;
                //combo

                print("signal : " + comboSignal.ToString("x") + " counCount = " + comboCount);
                //combo 저장
                comboSignal = comboSignal << 1;
                comboSignal += (uint)(isHand ? 1 : 0);

                ComboTimer = 0;
                comboCount++;
                if (comboCount == 6 && comboSignal == 0x3C)
                {
                    CompAnimation.AniFlipKick();
                    AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                    AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);

                }
                else if (comboCount == 7 && comboSignal == 0x78)
                {
                    //print(ComboCount + ", " + ComboSignal.ToString("x"));
                    //spacialKick
                    AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                    AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);
                    CompAnimation.AniSpacialKick();
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

                    CompAnimation.AniAttack(isHand);

                    //손 번갈아공격
                    isLeft = !isLeft;

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
                    targetObject.Move(Vector3.up * targetObject.JumpForce * 0.5f + Vector3.forward * 10);
                    CompAnimation.AniJumpKick();
                    //점프킥


                }
            }
        }
    }

    public override void Jump()
    {
        CompAnimation.AniJump();
    }


}
