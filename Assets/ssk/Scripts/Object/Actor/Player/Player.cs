using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//public enum EquipmentState
//{
//    Eq_Fight = 0,
//    Eq_Sword = 1
//};
//public enum AttackColliderIndex
//{
//    ACI_LeftFoot=0,
//    ACI_RightFoot = 1,
//    ACI_LeftHand = 2,
//    ACI_RightHand = 3,
//}



[RequireComponent(typeof(CharacterController))]
public class Player : Actor
{


    protected Dictionary<EEquipmentState, BaseBehavior> listBehavior;
    //유저 케릭터의 공격 클래스
    protected BaseBehavior behavior;
    public BaseBehavior Behavior
    {
        get
        {
            return behavior;
        }
    }

    //debug
    //public List<float> DebugFloat;
    //public List<Vector3> DebugVector;
    //public List<bool> DebugBool;
    //ani state
    protected EEquipmentState NowEq;

    protected int TotalComboCount;


    //components
    protected CharacterController CompCharCon;


    //[Serializable]
    //public Animator[] CompAnimators;
    public List<RuntimeAnimatorController> CompAnimators;

    [SerializeField]
    protected TrailRenderer trail;
    // Use this for initialization
    void Start()
    {

        Init();
    }
    public override void Init()
    {
        
        base.Init();
        listBehavior = new Dictionary<EEquipmentState, BaseBehavior>();
        isG = new bool[2];

        beforePos = gameObject.transform.position;
        ListAttackColliders = new List<Collider>();
        ListAttackColliders.Add(FindInChild("Character1_LeftFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindInChild("Character1_RightFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindInChild("Character1_LeftHand").GetComponent<Collider>());
        ListAttackColliders.Add(FindInChild("Character1_RightHand").GetComponent<Collider>());
        ListAttackCollidersComp = new List<AttackCollider>();
        ListAttackCollidersComp.Add(FindInChild("Character1_LeftFoot").GetComponent<AttackCollider>());
        ListAttackCollidersComp.Add(FindInChild("Character1_RightFoot").GetComponent<AttackCollider>());
        ListAttackCollidersComp.Add(FindInChild("Character1_LeftHand").GetComponent<AttackCollider>());
        ListAttackCollidersComp.Add(FindInChild("Character1_RightHand").GetComponent<AttackCollider>());


        Animator CompAnimator = gameObject.GetComponent<Animator>();
        behavior = gameObject.AddComponent<FightBehavior>();

        behavior.Init(this, CompAnimator,name);
        GetComponentsInit();
        power = 100;
        hp = 10000;
        mp = 300;
        nowPower = power;
        nowHp = hp;
        nowMp = mp;
    }
    protected Vector3 beforePos;
    protected Vector3 vel;
    public override Vector3 Velocity
    {
        //get { print(moveDirection + "<=>" + CompCharCon.velocity); return moveDirection + CompCharCon.velocity; }
        get
        {

            //print(vel);
            return vel;
        }
        set { moveDirection = value; }
    }

    protected void GetComponentsInit()
    {
        CompCharCon = GetComponent<CharacterController>();
        Rigidbody rigid = GetComponent<Rigidbody>();
        print(rigid);


    }
    // Update is called once per frame
    void Update()
    {


        UpdatePlayer();

    }
    private void LateUpdate()
    {

    }
    protected virtual void FixedUpdate()
    {

        FixedUpdatePlayer();

    }

    //업데이트
    protected virtual void UpdatePlayer()
    {
        //print("Power:" + nowPower);

        vel = (transform.position - beforePos) / Time.deltaTime;
        beforePos = transform.position;
        if (CompCharCon.isGrounded)
        {

            if (moveDirection.y < 0)
                moveDirection.y = -0.001f;
        }
        else
        {
            // Apply gravity    
            if (moveDirection.y > 0)
                moveDirection += Physics.gravity * Time.deltaTime;

            //moveDirection.y = 0;
        }


        // Move the controller    
        Vector3 dir = moveDirection * Time.deltaTime;

        isG[0] = CompCharCon.isGrounded;

        Vector3 mry = moveDirection;
        mry.y = 0;
        sqrtVel = mry.sqrMagnitude;
        CompCharCon.Move(dir);


        isG[1] = CompCharCon.isGrounded;
        isGrounded = isG[0] | isG[1];

        moveDirection.x = 0;
        moveDirection.z = 0;

    }



    //fixed업데이트
    protected virtual void FixedUpdatePlayer()
    {

    }
    public override void Rolling()
    {
        trail.enabled = true;
        base.Rolling();
        behavior.Rolling();

    }
    public override void EndRolling()
    {
        base.EndRolling();
        trail.enabled = false;
    }
    public override void Move(Vector3 vec)
    {


        moveDirection += vec * NowMoveSpeed; //transform.TransformDirection(vec)* MoveSpeed;
        Vector3 vel2d = vel;
        vel2d.y = 0;
        //print("player'sMove : " + vec + "2d vel" + vel2d);
        //if (vel.y < 0.1f && IsGrounded && vel2d.sqrMagnitude > 0.1f)
        if (vel.y < 0.0f && IsGrounded && vel2d.sqrMagnitude > 0.1f)
        {
            print("vel.y" + vel.y + "vel2d.sqrMagnitude"+ vel2d.sqrMagnitude);
            if (vec.sqrMagnitude < 0.1f)
            {
                print("Behavior.stop()");
                behavior.Stop();
            }
            else
                behavior.Move();
        }
    }
    public void Stop()
    {
        moveDirection = Vector3.zero;
        behavior.Stop();

    }
    public void AttackA()
    {
        behavior.AttackA();
    }
    public void AttackB()
    {
        behavior.AttackB();
    }
    public virtual void Skill(int charged)
    {
        int useMp = (charged / 100) * 100;
        if (nowMp >= useMp)
        {
            print("now MP : " + nowMp + "RemoveMP : " + useMp);
            nowMp -= useMp;
            behavior.Skill(charged);
        }
    }
    public override void onDamaged(int damage)
    {
        if (isAlive == true)
        {
            base.onDamaged(damage);
            if (isRolling == false)
                behavior.onDamage(damage);

        }

    }
    public override void onDead()
    {
        behavior.Dead();
        base.onDead();
    }

    public void Rotate(Vector3 angle)
    {
        //transform.Rotate(angle* RotateSpeed);
    }
    public void Jump()
    {
        if (!behavior.IsAnimationPlaing && !behavior.IsJumping)
        {
            moveDirection += Vector3.up * JumpForce;
            behavior.Jump();
        }

    }

    public virtual void switchEq(EEquipmentState Eq)
    {
        print("switchEq  = " + Eq.ToString());
        if (listBehavior.ContainsKey(this.NowEq) == false)
        {
            listBehavior.Add(this.NowEq, behavior);
        }
        
        BaseBehavior be;
        if (listBehavior.TryGetValue(Eq, out be) == false)
        {
            AddEq(Eq);
        }
        behavior.switchEq(Eq, CompAnimators[(int)Eq]);
        this.NowEq = Eq;
    }
    public virtual void AddEq(EEquipmentState NowEq)
    {
        print("AddEQ  = " + NowEq.ToString());
        string beName = "";
        switch (NowEq)
        {
            case EEquipmentState.CharEqState_Fight:
                behavior=gameObject.AddComponent<FightBehavior>();
                break;
            case EEquipmentState.CharEqState_Sword:
                behavior=gameObject.AddComponent<WeaponBehavior>();
                break;
            case EEquipmentState.CharEqState_Gun:
                behavior = gameObject.AddComponent<WeaponBehavior>();
                beName = "Gun";
                break;
            case EEquipmentState.CharEqState_Pistol:
                behavior = gameObject.AddComponent<WeaponBehavior>();
                beName = "Pistol";
                break;
            case EEquipmentState.CharEqState_End:
                break;
            
                
            default:
                break;
        }
        behavior.Init(this, gameObject.GetComponent<Animator>(),beName);

    }
}
