using System.Collections;
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
    protected bool isJumpPunching;
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
    
    public void AniPlayStart()
    {
        isAnimationPlaing = true;
    }
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
        if (!isAnimationPlaing)
        {
            nextAttack = null;
            Attack(true);
        }
    }
    public override void AttackB()
    {
        if (!isAnimationPlaing)
        {
            nextAttack = null;
            Attack(false);
        }
    }
    public void Upper()
    {
        targetObject.Move(Vector3.up * targetObject.JumpForce * 0.1f + Vector3.forward);
    }
    public override void Skill(int charged)
    {
        
        if (charged >=300)
        {
            //3필
            targetObject.NowAttackPower = 1.0f;
            targetObject.NowMoveSpeed = 0;


        }
        else if (charged >= 200)
        {
            
            //2필
            targetObject.PowerUp(2f);
            targetObject.Invoke("PowerReset", 10.0f);
        }
        else if (charged >= 100)
        {
            //1필
            targetObject.NowAttackPower = 5.0f;
            AttackColliderEnable(EAttackColliderIndex.ACI_LeftHand);
        }
        else
        {
            return;
        }
        Stop();

        ani.AniSkill(charged);
        isAnimationPlaing = true;
    }
    public override void Stop()
    {
        base.Stop();
        isJumpKickDowning = false;
        isJumpKicking = false;
        isJumping = false;
        isDownnig = false;
        isJumpPunching = false;
        //ani.AniStop();
        
    }

    bool isDownnig = false;
    protected override void Update()
    {
        base.Update();
        //print( "isJumping , isDowing : " + isJumping +isDownnig);
        if (isJumping)
        {
            //print(targetObject.Velocity);

            if (isDownnig && targetObject.IsGrounded)
            {
                Stop();
                ani.AniJumpEnd();
                //isJumping = false;
                //isDownnig = false;

            }
            else if (targetObject.Velocity.y < -0.1f)
            {
                isDownnig = true;
            }
        }
        if (isJumpPunching)
        {
            //print("--");
            targetObject.Move(Physics.gravity * Time.deltaTime);
            if (targetObject.IsGrounded && isAnimationPlaing && targetObject.Velocity.y<-0.1f)
            {
                Stop();

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

            targetObject.Move(Physics.gravity * 2f * Time.deltaTime);
            Vector3 dir2 = transform.forward;
            dir2.y = 0;
            targetObject.Move(dir2 * Time.deltaTime * 200f);
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
        if (isAnimationPlaing && nextAttack == null)
        {
            nextAttack = new NextAttack (isHand);
        }
        else
        {
            isAnimationPlaing = true;

            if (!IsJumping)
            {
                targetObject.Velocity = Vector3.zero;
                //combo

                print("signal : " + comboSignal.ToString("x") + " counCount = " + comboCount + "isleft"+ isLeft);
                //combo 저장
                comboSignal = comboSignal << 1;
                comboSignal += (uint)(isHand ? 1 : 0);

                ComboTimer = 0;
                comboCount++;
                if (comboCount == 6 && comboSignal == 0x3C)
                {
                    //ani.AniFlipKick();
                    targetObject.NowAttackPower = 3.0f;
                    ani.SendMessage("AniFlipKick");
                    AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                    AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);
                    
                }
                else if (comboCount == 7 && comboSignal == 0x78)
                {
                    //print(ComboCount + ", " + ComboSignal.ToString("x"));
                    //spacialKick
                    targetObject.NowAttackPower = 4.0f;
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
                            targetObject.NowAttackPower = 1.0f;
                            //애니메이션LH
                            AttackColliderEnable(EAttackColliderIndex.ACI_LeftHand);
                        }
                        else
                        {
                            targetObject.NowAttackPower = 1.3f;
                            //애니메이션LF
                            AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                        }
                    }
                    else
                    {
                        //애니메이션R
                        if (isHand)
                        {
                            targetObject.NowAttackPower = 1.0f;
                            //애니메이션RH
                            AttackColliderEnable(EAttackColliderIndex.ACI_RightHand);
                        }
                        else
                        {
                            targetObject.NowAttackPower = 1.3f;
                            //애니메이션RF
                            AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);
                        }
                    }
                    
                    //ani.AniAttack(isHand);
                    ani.SendMessage("AniAttack",isHand);
                    isLeft = !isLeft;
                    //손 번갈아공격


                }
            }
            else
            {
                //공중공격
                if (isHand)
                {
                    //점프손
                    ani.AniStop();
                    targetObject.Move(Vector3.up * targetObject.JumpForce * 0.2f + Vector3.forward);
                    ani.SendMessage("AniJumpPunch");
                    AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);
                    AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                    targetObject.NowAttackPower = 2.0f;
                    isJumpPunching = true;
                }
                else
                {
                    targetObject.Move(Vector3.up * targetObject.JumpForce * 0.05f + Vector3.forward );
                    //ani.AniJumpKick();
                    ani.SendMessage("AniJumpKick");
                    AttackColliderEnable(EAttackColliderIndex.ACI_RightFoot);
                    AttackColliderEnable(EAttackColliderIndex.ACI_LeftFoot);
                    targetObject.NowAttackPower = 3.0f;
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
