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
    EEquipmentState NowEq;

    protected int TotalComboCount;
    

    //components
    protected CharacterController CompCharCon;

    
    //[Serializable]
    //public Animator[] CompAnimators;
    public List<RuntimeAnimatorController> CompAnimators;
    
    // Use this for initialization
    void Start () {
        Init();
    }
    public override void Init()
    {
        base.Init();
        
        isG = new bool[2];

        beforePos = gameObject.transform.position;
        ListAttackColliders = new List<Collider>();
        ListAttackColliders.Add(FindTrans("Character1_LeftFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_RightFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_LeftHand").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_RightHand").GetComponent<Collider>());

        Animator CompAnimator = gameObject.GetComponent<Animator>();
        behavior = gameObject.AddComponent<FightBehavior>();
        behavior.Init(this, CompAnimator);
        GetComponentsInit();

    }
    Vector3 beforePos;
    public override Vector3 Velocity
    {
        //get { print(moveDirection + "<=>" + CompCharCon.velocity); return moveDirection + CompCharCon.velocity; }
        get {Vector3 vel= (transform.position- beforePos);print(vel); return vel; }
        set { moveDirection = value; }
    }

    protected void GetComponentsInit()
    {
        CompCharCon = GetComponent<CharacterController>();
        
    }
	// Update is called once per frame
	void Update () {

        
        UpdatePlayer();
        beforePos = gameObject.transform.position;
    }
    private void LateUpdate()
    {
        
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
            if(moveDirection.y>0)
                moveDirection += Physics.gravity* Time.deltaTime;
            
            //moveDirection.y = 0;
        }

        
        // Move the controller    
        Vector3 dir = moveDirection * Time.deltaTime;

        isG[0]=CompCharCon.isGrounded;

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
    protected void FixedUpdatePlayer()
    {
    }

    public override void Move(Vector3 vec)
    {
        moveDirection += vec * MoveSpeed; //transform.TransformDirection(vec)* MoveSpeed;
        if (vec.y < 0.1f && IsGrounded)
        {
            if (vec.sqrMagnitude < 0.1f)
                behavior.Stop();
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
    public void Skill(int charged)
    {
        behavior.Skill(charged);
    }
    public override void onDamaged(int damage)
    {
        base.onDamaged(damage);
        
    }
    public override void onDead()
    {
        base.onDead();
    }

    public void Rotate(Vector3 angle)
    {
        //transform.Rotate(angle* RotateSpeed);
    }
    public void Jump()
    {
        if (!behavior.IsAttacking && !behavior.IsJumping)
        {
            moveDirection += Vector3.up * JumpForce;
            behavior.Jump();
        }
        
    }
    
   

}
