using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//입력을 받는 플레이어
//싱글턴
public partial class PlayablePlayer : Player
{
    Color LightColor;
    bool LightSet = false;
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

    float timeScale = 1;
    float lerpTime = 0;
    bool isDark;
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
            else if (isGrounded && !behavior.IsJumping)
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
            ButtonClick(EButtonList.EBL_Jump);
        }
        else
        {
            ButtonRelease(EButtonList.EBL_Jump);
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
            timeScale = 1;
            



        }
        
        if (Input.GetKey(KeyCode.C))
        {
            //lerpTime = ;
            if (skill > 100)
            {
                //암전
                if (isDark == false)
                {
                    if (LightSet == false)
                    {
                        LightColor = StageManager.MainLight.color;
                        LightSet = true;
                    }
                    StageManager.MainLight.color = new Color(1f, 0.1f, 0.1f);
                    StageManager.MainLight.intensity = 3;
                    isDark = true;
                }

                timeScale = Mathf.Lerp(timeScale, 0.01f, Time.unscaledDeltaTime * 3f);
                Time.timeScale = timeScale;
            }
            //charge
            skill += Time.unscaledDeltaTime * 300;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            //skill
            timeScale = 1;
            Time.timeScale = timeScale;
            print(skill);
            behavior.Skill((int)skill);
            skill = 0;
            //원상복구
            if (isDark)
            {
                Time.timeScale = 1;
                StageManager.MainLight.color = LightColor;
                StageManager.MainLight.intensity = 1;
                isDark = false;
            }
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

    public void ButtonClick(EButtonList eButtonList)
    {
        switch (eButtonList)
        {
            case EButtonList.EBL_AttackA:
                attack1 = 1;
                break;
            case EButtonList.EBL_AttackB:
                attack2 = 1;
                break;
            case EButtonList.EBL_Skill:
                skill = 1;
                break;
            case EButtonList.EBL_Jump:
                jump = 1;
                break;
            default:
                break;
        }
    }
    public void ButtonRelease(EButtonList eButtonList)
    {
        switch (eButtonList)
        {
            case EButtonList.EBL_AttackA:
                attack1 = 0;
                break;
            case EButtonList.EBL_AttackB:
                attack2 = 0;
                break;
            case EButtonList.EBL_Skill:
                skill = 0;
                break;
            case EButtonList.EBL_Jump:
                jump = 0;
                break;
            default:
                break;
        }
    }
}
