using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : BaseAnimation {

	// Use this for initialization
	void Start () {
		
	}
    public override void AniAttackA()
    {
        CompAnimator.SetTrigger("AttackA");

    }
    public override void AniAttackB()
    {
        //던지기!
        //base.AniAttackB();
        //CompAnimator.SetTrigger("AttackB");
    }
    public override void AniSkill(int charged)
    {
        base.AniSkill(charged);
        CompAnimator.SetTrigger("AttackC");
    }
}
