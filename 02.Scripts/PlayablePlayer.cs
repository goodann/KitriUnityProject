using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//입력을 받는 플레이어
//싱글턴
public class PlayablePlayer : Player {

    //singleton
    private static PlayablePlayer Instance=null;
    public PlayablePlayer GetInstance()
    {
        if (Instance == null)
        {
            //lock
            if (Instance == null) {
                Instance = new PlayablePlayer();
            }
        }
        return Instance;
    }
    /// <summary>
    /// 
    /// </summary>

    private PlayablePlayer()
    {

    }
    // Use this for initialization
    void Start () {
        ComboInit();
        GetComponentsInit();
    }

    // Update is called once per frame
    void Update () {
        
        UpdatePlayer();
    }
    private void FixedUpdate()
    {
        ControllInput();
        FixedUpdatePlayer();
    }
    void ControllInput()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        //float attack1 = Input.GetAxis("Fire1");
        //float attack2 = Input.GetAxis("Fire2");
        //float jump = Input.GetAxis("Jump");

        float attack1=0;
        float attack2=0;
        float jump=0;

        if(Input.GetKeyDown(KeyCode.Space)){
            jump = 1;
        }


        Vector3 moveVec = new Vector3();
        //moveVec = Vector3.right * hInput;
        moveVec = Vector3.forward * vInput;
        Move(moveVec);
        Rotate(new Vector3(0, hInput, 0));
        if (jump != 0)
        {
            Jump();
        }
        if (attack1!=0)
        {
            Attack(true);
        }
        if (attack2 != 0)
        {
            Attack(false);
        }

    }
}
