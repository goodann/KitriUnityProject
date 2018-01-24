using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PlayerState
{
    PsStay=0,
    PsMoving=1,
    Ps
}
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

    
    bool isLeft;

    int ComboCount;
    uint ComboSignal;
    int TotalComboCount;
    float ComboTimer;
    Vector3 moveDirection;

    //components
    CharacterController CompCharCon;

    public float JumpForce = 10;
    public float MoveSpeed = 10;
    public float RotateSpeed = 2;
    // Use this for initialization
    void Start () {
        ComboInit();
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

    }

    //fixed업데이트
    protected void FixedUpdatePlayer()
    {
        if (CompCharCon.isGrounded)
        {
            if (moveDirection.y < 0)
                moveDirection.y = 0;
        }
        else
        {
            // Apply gravity    
            moveDirection += Physics.gravity * Time.fixedDeltaTime;
            
        }
        // Move the controller    
        Vector3 dir = moveDirection * Time.fixedDeltaTime;
        CompCharCon.Move(dir);
        moveDirection -= dir;
        moveDirection.x = 0;
        moveDirection.z = 0;
        if (ComboTimer > 2.0f)
        {
            ComboTimer = 0;
            ComboInit();
        }

        ComboTimer += Time.fixedDeltaTime;
    }
    public void Move(Vector3 vec)
    {
        //CompCharCon.Move(vec);
        moveDirection += transform.TransformDirection(vec)* MoveSpeed;
    }
    public void Rotate(Vector3 angle)
    {
        transform.Rotate(angle* RotateSpeed);
    }
    public void Jump()
    {
        //CompCharCon.Move(Vector3.up);
        moveDirection += Vector3.up* JumpForce;
    }
    public void Attack(bool isHand)
    {
        if (isLeft)
        {
            //애니메이션L
            if (isHand) {
                //애니메이션LH
            }
            else
            {
                //애니메이션LF
            }
        }
        else
        {
            //애니메이션R
            if (isHand)
            {
                //애니메이션RH
            }
            else
            {
                //애니메이션RF
            }
        }


        //손 번갈아공격
        isLeft = !isLeft;

        //combo 저장
        ComboSignal += (uint)(isHand ? 1 : 0);
        ComboSignal=ComboSignal << 1;
        ComboTimer = 0;
    }
    public void ComboInit()
    {
        //콤보초기화
        TotalComboCount = 0;
        ComboSignal = 0;
        ComboCount = 0;
        ComboTimer = 0.0f;
    }
}
