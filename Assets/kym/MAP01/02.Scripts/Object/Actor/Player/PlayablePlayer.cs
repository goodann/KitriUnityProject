﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//입력을 받는 플레이어
//싱글턴
public partial class PlayablePlayer : Player
{

    //singleton
    private static PlayablePlayer Instance = null;
    public PlayablePlayer GetInstance()
    {
        if (Instance == null)
        {
            //lock
            if (Instance == null)
            {
                Instance = new PlayablePlayer();
            }
        }
        return Instance;
    }
    /// <summary>
    /// 
    /// </summary>

    float hInput = 0;
    float vInput = 0;
    //float attack
    //float attack
    //float jump =

    public float attack1 = 0;
    public float attack2 = 0;
    public float jump = 0;


    private PlayablePlayer()
    {

    }
    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ControllInput();
        UpdatePlayer();
        //DebugFloat[0] = hInput;
        //DebugFloat[1] = vInput;
        //DebugFloat[2] = attack1;
        //DebugFloat[3] = attack2;
        //DebugFloat[4] = jump;
    }
    private void FixedUpdate()
    {

        FixedUpdatePlayer();
    }
    void ControllInput()
    {
        if (!combat.IsAttacking)
        {
            //hInput = Input.GetAxis("Horizontal");
            //vInput = Input.GetAxis("Vertical");
            hInput = UIJoyStick.InputValue.x;
            vInput = UIJoyStick.InputValue.y;

        }
        else
        {
            hInput = 0;
            vInput = 0;
        }
        //attack1 = Input.GetAxis("Fire1");
        //attack2 = Input.GetAxis("Fire2");
        //jump = Input.GetAxis("Jump");


        //attack1 = 0;
        //attack2 = 0;
        //jump = 0;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    jump = 1;
        //}
        //else
        //{
        //    jump = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    attack2 = 1;
        //}
        //else
        //{
        //    attack2 = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    attack1 = 1;
        //}
        //else
        //{
        //    attack1 = 0;
        //}



        Vector3 moveVec = new Vector3();
        //moveVec = Vector3.right * hInput;



        //transform.transform.rotation = Quaternion.Euler(Vector3.up * hInput*90);
        
        moveVec = Vector3.right * hInput * MoveSpeed;
        moveVec += Vector3.forward * vInput * MoveSpeed;
        transform.transform.LookAt(transform.transform.position + moveVec);
        Move(moveVec);
        //moveDirection += moveVec;
        //Rotate(new Vector3(0, hInput, 0));
        if (jump != 0)
        {
            Jump();
        }
        if (attack1 != 0)
        {
            //combat.Attack(true);
            combat.Attack1();
        }
        if (attack2 != 0)
        {
            combat.Attack2();
        }

    }


   
}