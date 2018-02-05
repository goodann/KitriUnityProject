using System.Collections;
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

        base.Init(target, animator);


        ani = gameObject.AddComponent<FightAnimation>();
        ani.AnimatorInit(target, this, animator);
        EndAttack();

    }
    public override void Skill(int charged)
    {
        throw new System.NotImplementedException();
    }
    public override void AttackA()
    {
        throw new System.NotImplementedException();
    }
    public override void AttackB()
    {
        throw new System.NotImplementedException();
    }
    public override void Damaged(int damage)
    {
    }
}
