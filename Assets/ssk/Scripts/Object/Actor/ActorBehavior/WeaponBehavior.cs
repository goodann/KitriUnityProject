﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : BaseBehavior {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

	}
    public override void Init(Actor target, Animator animator)
    {
        ComboInit();
        base.Init(target, animator);


        ani = gameObject.AddComponent<WeaponAnimation>();
        ani.AnimatorInit(target, this, animator);
        EndAttack();

    }
    public override void Skill(int charged)
    {
        //throw new System.NotImplementedException();

        if (charged >= 300)
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
    public override void AttackA()
    {
        //throw new System.NotImplementedException();
        if (!isAnimationPlaing)
        {
            isAnimationPlaing = true;
            ani.AniAttackA();
            comboCount++;
            if (comboCount >= 3)
                comboCount = 0;

        }
        
    }
    public override void AttackB()
    {
        //throw new System.NotImplementedException();
        ani.AniAttackB();
    }
    public override void Damaged(int damage)
    {
    }
    public override void Jump()
    {
        base.Jump();
    }
    public void ComboInit()
    {
        //콤보초기화
        comboCount = 0;
        ComboTimer = 0.0f;
    }
}

