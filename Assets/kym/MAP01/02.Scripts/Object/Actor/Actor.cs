using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Status
{
    public int hp;
    public int mp;
    public int power;
}

[RequireComponent(typeof(Animator))]
public class Actor : MyBaseObejct {
    //public 변수

    public float JumpForce = 100;
    public float MoveSpeed = 2;

    //공격판정 컬라이더 목록
    public List<Collider> ListAttackColliders;

    //protected 변수
    //protected Animator CompAnimator;
    protected Vector3 moveDirection;
    
    protected float sqrtVel;

    //지상 공중 판정
    protected bool[] isG;
    protected bool isGrounded;



    //status
    protected int hp;
    protected int mp;
    protected int power;


    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public int MP
    {
        get { return mp; }
        set { mp = value; }
    }
    public int POWER
    {
        get { return power; }
        set { power = value; }
    }

    //property
    public virtual Vector3 Velocity
    {
        get{ return moveDirection; }
        set{moveDirection = value;}
    }
    public float SqrtVel
    {
        get { return sqrtVel; }
    }

    public bool IsGrounded
    {
        get{return isGrounded;}
        
    }
    public virtual void StatusInit(Status st)
    {
        hp = st.hp;
        mp = st.mp;
        power = st.power;
    }


    public virtual void Init()
    {
        
    }

    public virtual void Move(Vector3 dir)
    {
        moveDirection += dir;
    }

    public virtual void Attack()
    {

    }
    public virtual void Skill()
    {

    }
    public virtual void onDamaged(int damage)
    {
        hp-=damage;
        if (hp < 0)
            onDead();
    }
    public virtual void onDead()
    {

    }
}
