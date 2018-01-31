using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    public float aggroRadius;
    public float maxAggroRadius;
    public float attackRange;
    public float RecoveryTime;

    private bool life = true;
    private Transform mobTR;
    private Transform playerTR;
    private Animator animator;

    private IState currentState;
    private FollowState followState;
    private IdleState idleState;
    private StunState stunState;
    private AttackState attackState;

    private NavMeshAgent navAgent;

    public Transform MobTR
    {
        get { return mobTR; }
        set { mobTR = value; }
    }

    public Transform PlayerTR
    {
        get { return playerTR; }
        set { playerTR = value; }
    }

    public Animator Animator { get { return animator; } }
    public NavMeshAgent NavAgent { get { return navAgent; } }

    public FollowState FollowState { get { return followState; } }
    public IdleState IdleState { get { return idleState; } }
    public StunState StunState { get { return stunState; } }
    public AttackState AttackState { get { return attackState; } }

    public bool Life
    {
        get { return life; }
        set { life = value; }
    }

    private void Awake()
    {
        idleState = new IdleState();
        followState = new FollowState();
        attackState = new AttackState();
        stunState = new StunState();
    }

    void Start()
    {
        Init();

        mobTR = GetComponent<Transform>();
        playerTR = GameObject.FindWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        ListAttackColliders = new List<Collider>();
        ListAttackColliders.Add(FindInChild("AttackColliderLeftArm").GetComponent<Collider>());
        ListAttackColliders.Add(FindInChild("AttackColliderRightArm").GetComponent<Collider>());

        ChangeState(idleState);
    }

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;

        currentState.Enter(this);
        Debug.Log("Enter State : " + newState.ToString());
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override object GetData(string keyData, params object[] datas)
    {
        return base.GetData(keyData, datas);
    }

    public override void Init()
    {
        hp = 10;
        mp = 0;
        power = 3;
    }

    public override void onDamaged(int damage)
    {
        Debug.Log("onDamaged : " + damage);
        hp -= damage;
        
        if(hp <= 0)
        {
            hp = 0;
            ChangeState(StunState);
        }
        else
        {
            if(damage >= 5)
            {
                animator.SetTrigger("CriticalHit");
            }
            else
            {
                animator.SetTrigger("Hit");
            }
        }    
    }

    public override void onDead()
    {
        base.onDead();
    }

    public override void Skill()
    {
        base.Skill();
    }

    public override void ThrowEvent(string keyData, params object[] datas)
    {
        base.ThrowEvent(keyData, datas);
    }

    public void Hit()
    {
        foreach (Collider coll in ListAttackColliders)
        {
            coll.gameObject.SetActive(true);
        }

        return;
    }

    public void HitEnd()
    {
        foreach (Collider coll in ListAttackColliders)
        {
            coll.gameObject.SetActive(false);
        }

        return;
    }
}
