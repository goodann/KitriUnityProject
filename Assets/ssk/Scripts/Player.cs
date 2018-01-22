using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EquipmentState
{
    Eq_Fight = 0,
    Eq_Sword = 1
};
public enum AttackColliderIndex
{
    ACI_LeftFoot=0,
    ACI_RightFoot = 1,
    ACI_LeftHand = 2,
    ACI_RightHand = 3,
}


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public partial class Player : MonoBehaviour {

    //debug
    public List<float> DebugFloat;
    public List<Vector3> DebugVector;
    public List<bool> DebugBool;

    public bool[] isG;
    public bool isGrounded;
    //ani state
    EquipmentState NowEq;


    //attack

    protected bool isAttacking;

    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    protected bool isLeft;
    protected bool isJumpKicking;
    protected bool isJumpKickDowning;

    protected int ComboCount;
    protected uint ComboSignal;
    protected int TotalComboCount;
    protected float ComboTimer;
    protected Vector3 moveDirection;
    protected float sqrtVel;

    //components
    protected CharacterController CompCharCon;
    protected Animator CompAnimator;
    
    //[Serializable]
    //public Animator[] CompAnimators;
    public List<RuntimeAnimatorController> CompAnimators;
    public List<Collider> ListAttackColliders;


    public float JumpForce = 7;
    public float MoveSpeed = 2;
    
    // Use this for initialization
    void Start () {
        Init();
    }
    public Transform FindTrans(string name)
    {
        return _FindTrans(name, transform);
    }
    public Transform _FindTrans(string name,Transform trans)
    {
        if (name == trans.name)
            return trans;
        if (trans.childCount == 0)
        {
            return null;
        }
        else
        {
            for (int i = 0; i < trans.childCount; ++i)
            {
                Transform reTrans = _FindTrans(name, trans.GetChild(i));
                if (reTrans != null)
                    return reTrans;
            }
        }
        return null;
    }
    protected void Init()
    {
        isLeft = true;
        isG = new bool[2];
        ComboInit();
        GetComponentsInit();
        AnimatorInit();
        ListAttackColliders = new List<Collider>();
        
        ListAttackColliders.Add(FindTrans("Character1_LeftFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_RightFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_LeftHand").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_RightHand").GetComponent<Collider>());
    }



    protected void GetComponentsInit()
    {
        CompCharCon = GetComponent<CharacterController>();
        
    }
	// Update is called once per frame
	void Update () {
        UpdatePlayer();
    }
    void FixedUpdate()
    {
        FixedUpdatePlayer();
    }

    //업데이트
    protected void UpdatePlayer()
    {
        

        if (CompCharCon.isGrounded)
        {
            
            if (moveDirection.y < 0)
                moveDirection.y = -0.001f;
        }
        else
        {
            // Apply gravity    
            moveDirection += Physics.gravity* Time.deltaTime;

            if (isJumpKicking)
            {
                moveDirection += Physics.gravity *1.5f* Time.deltaTime;
            }
            if (isJumpKickDowning)
            {

                moveDirection += Physics.gravity * 1f * Time.deltaTime;
                Vector3 dir2 = transform.forward;
                dir2.y = 0;
                moveDirection += dir2 * Time.deltaTime * 300f;
            }

        }

        
        // Move the controller    
        Vector3 dir = moveDirection * Time.deltaTime;

        isG[0]=CompCharCon.isGrounded;
        //if (dir.y == 0)
        //{
        //    isG = true;
        //}
        Vector3 mry = moveDirection;
        mry.y = 0;
        sqrtVel = mry.sqrMagnitude;

        AniUpdate();
        //print(moveDirection);
        
            //print("sqrtVel" + sqrtVel);
        if (ComboTimer > 10.0f)
        {
            ComboTimer = 0;
            ComboInit();
        }

        //print("dir="+ moveDirection);
        //moveDirection -= dir;
        DebugVector[0] = CompCharCon.velocity;
        DebugVector[1] = moveDirection;

        DebugBool[0] = CompCharCon.isGrounded;
        CompCharCon.Move(dir);
        DebugBool[1] = CompCharCon.isGrounded;
        isG[1] = CompCharCon.isGrounded;
        isGrounded = isG[0] | isG[1];

        moveDirection.x = 0;
        moveDirection.z = 0;
        ComboTimer += Time.deltaTime;
        DebugFloat[5] = ComboTimer;
        DebugBool[2] = isAttacking;


    }
    public void EndAttack()
    {
        isAttacking = false;
        foreach( var  i in ListAttackColliders)
        {
            i.enabled = false;
        }

    }
    public void AttackColliderEnable(AttackColliderIndex index)
    {
        ListAttackColliders[(int)index].enabled = true;
    }

    //fixed업데이트
    protected void FixedUpdatePlayer()
    {
    }
    public void Move(Vector3 vec)
    {
        moveDirection += vec * MoveSpeed; //transform.TransformDirection(vec)* MoveSpeed;
    }
    public void Rotate(Vector3 angle)
    {
        //transform.Rotate(angle* RotateSpeed);
    }
    public void Jump()
    {
        moveDirection += Vector3.up* JumpForce;
        AniJump();
    }
    public void Attack(bool isHand)
    {
        if (isAttacking)
        {
            
        }
        else
        {
            isAttacking = true;
            
            if (isGrounded)
            {
                moveDirection = Vector3.zero;
                //combo


                //combo 저장
                ComboSignal = ComboSignal << 1;
                ComboSignal += (uint)(isHand ? 1 : 0);

                ComboTimer = 0;
                ComboCount++;
                DebugFloat[6] = ComboSignal;
                DebugFloat[7] = ComboCount;
                if (ComboCount == 6 && ComboSignal == 0x3C)
                {
                    AniFlipKick();
                    AttackColliderEnable(AttackColliderIndex.ACI_LeftFoot);
                    AttackColliderEnable(AttackColliderIndex.ACI_RightFoot);

                }
                else if (ComboCount == 7 && ComboSignal == 0x78)
                {
                    //print(ComboCount + ", " + ComboSignal.ToString("x"));
                    //spacialKick
                    AttackColliderEnable(AttackColliderIndex.ACI_LeftFoot);
                    AttackColliderEnable(AttackColliderIndex.ACI_RightFoot);
                    AniSpacialKick();
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
                            AttackColliderEnable(AttackColliderIndex.ACI_LeftHand);
                        }
                        else
                        {
                            //애니메이션LF
                            AttackColliderEnable(AttackColliderIndex.ACI_LeftFoot);
                        }
                    }
                    else
                    {
                        //애니메이션R
                        if (isHand)
                        {
                            //애니메이션RH
                            AttackColliderEnable(AttackColliderIndex.ACI_RightHand);
                        }
                        else
                        {
                            //애니메이션RF
                            AttackColliderEnable(AttackColliderIndex.ACI_RightFoot);
                        }
                    }

                    AniAttack(isHand);

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
                    moveDirection += Vector3.up * JumpForce * 0.5f + Vector3.forward * 10;
                    AniJumpKick();
                    //점프킥


                }
            }
        }
    }
    public void JumpKick()
    {
        isJumpKickDowning = true;

        jumpKickQua= transform.rotation* Quaternion.Euler(30, 0, 0);
        jumpKickQuaOrign = transform.rotation;
        //Time.timeScale = 0.1f;
    }
    public void ComboInit()
    {
        //콤보초기화
        TotalComboCount = 0;
        ComboSignal = 0;
        ComboCount = 0;
        ComboTimer = 0.0f;
        isLeft = true;
    }
}
