using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//입력을 받는 플레이어
//싱글턴
public partial class PlayablePlayer : Player
{
    Color LightColor;
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

    float attack1 = 0;
    float attack2 = 0;
    float jump = 0;
    float skill = 0;

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
        if (!behavior.IsAttacking)
        {
            //hInput = Input.GetAxis("Horizontal");
            //vInput = Input.GetAxis("Vertical");
            hInput = UIJoyStick.InputValue.x;
            vInput = UIJoyStick.InputValue.y;
            Vector3 moveVec = new Vector3();
            moveVec = Vector3.right * hInput * MoveSpeed;
            moveVec += Vector3.forward * vInput * MoveSpeed;
            transform.transform.LookAt(transform.transform.position + moveVec);
            if (moveVec.sqrMagnitude > 0.1f)
                Move(moveVec);
            else if (isGrounded&&!behavior.IsJumping)
                Stop();
        }
        else
        {
            hInput = 0;
            vInput = 0;
            //Stop();
        }
        attack1 = Input.GetAxis("Fire1");
        attack2 = Input.GetAxis("Fire2");
        jump = Input.GetAxis("Jump");

            

        attack1 = 0;
        attack2 = 0;
        jump = 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = 1;
        }
        else
        {
            jump = 0;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            attack2 = 1;
        }
        else
        {
            attack2 = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {

            attack1 = 1;
        }
        else
        {
            attack1 = 0;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //skillstart
            skill = 0;
            //time느려짐
            Time.timeScale = 0.1f;
            //암전
            LightColor = StageManager.MainLight.color;
            StageManager.MainLight.color = new Color(1f, 0.1f, 0.1f);
            StageManager.MainLight.intensity = 2;

        }
        if (Input.GetKey(KeyCode.C))
        {
            //charge
            skill += Time.deltaTime * 300;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            //skill
            print(skill);
            behavior.Skill((int)skill);
            skill = 0;
            //원상복구
            Time.timeScale = 1;
            StageManager.MainLight.color = LightColor;
            StageManager.MainLight.intensity = 1;
        }

        //moveVec = Vector3.right * hInput;
        //transform.transform.rotation = Quaternion.Euler(Vector3.up * hInput*90);
        //moveDirection += moveVec;
        //Rotate(new Vector3(0, hInput, 0));

        if (jump != 0)
        {
            Jump();
        }
        if (attack1 != 0)
        {
            //behavior.Attack(true);
            behavior.AttackA();
        }
        if (attack2 != 0)
        {
            behavior.AttackB();
        }


    }
}
