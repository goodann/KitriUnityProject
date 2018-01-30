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
        
        
        
        ListAttackColliders = new List<Collider>();
        
        ListAttackColliders.Add(FindTrans("Character1_LeftFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_RightFoot").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_LeftHand").GetComponent<Collider>());
        ListAttackColliders.Add(FindTrans("Character1_RightHand").GetComponent<Collider>());

        CompAnimator = gameObject.GetComponent<Animator>();
        combat = gameObject.AddComponent<FightCombat>();
        combat.Init(this, CompAnimator);
        GetComponentsInit();

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

        BaseAnimation animation = combat.Animatoion;
        animation.AniUpdate();
        //combat.Animation.AniUpdate();

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
    }
    public void Rotate(Vector3 angle)
    {
        //transform.Rotate(angle* RotateSpeed);
    }
    public void Jump()
    {
        moveDirection += Vector3.up* JumpForce;
        combat.Jump();
        
    }
    void AniSwitchEq()
    {
        CompAnimator.runtimeAnimatorController = CompAnimators[(int)NowEq];
    }

}
