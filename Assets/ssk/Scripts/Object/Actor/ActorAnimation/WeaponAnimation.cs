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
        print("AniAttackA" + targetBehavior.ComboCount);
        switch (targetBehavior.ComboCount)
        {
            case 0:
                CompAnimator.SetTrigger("AttackA");
                break;
            case 1:
                CompAnimator.SetTrigger("AttackB");
                break;
            case 2:
                CompAnimator.SetTrigger("AttackC");
                break;
            case 3:

                break;
            default:
                break;
        }
        
        
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
