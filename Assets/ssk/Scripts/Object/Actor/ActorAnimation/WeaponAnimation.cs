using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : BaseAnimation {

	// Use this for initialization
	void Start () {
		
	}
    public override void AniAttackA()
    {
        base.AniAttackA();
        CompAnimator.SetTrigger("AttackA");
    }
    public override void AniAttackB()
    {
        base.AniAttackB();
        CompAnimator.SetTrigger("AttackB");
    }
    public override void AniSkill(int charged)
    {
        base.AniSkill(charged);
        CompAnimator.SetTrigger("AttackC");
    }

}
