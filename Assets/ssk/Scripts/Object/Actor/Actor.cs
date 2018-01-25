using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Actor : MyBaseObejct {
    protected Animator CompAnimator;
    //protected BaseAnimation Animation;
    public List<Collider> ListAttackColliders;

    protected BaseCombat combat;
    public BaseCombat Combat
    {
        get
        {
            return combat;
        }
    }


    public float JumpForce = 7;
    public float MoveSpeed = 2;

    public virtual void Init()
    {
        
    }


    protected Vector3 moveDirection;
    public Vector3 Velocity
    {
        get
        {
            return moveDirection;
        }
        set
        {
            moveDirection = value;
        }
    }
    protected float sqrtVel;
    public float SqrtVel
    {
        get { return sqrtVel; }
    }
    protected bool[] isG;
    protected bool isGrounded;
    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    }

    public virtual void Move(Vector3 dir)
    {
        moveDirection += dir;
    }
    
    
}
