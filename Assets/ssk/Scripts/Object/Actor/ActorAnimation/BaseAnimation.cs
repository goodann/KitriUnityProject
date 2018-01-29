using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimation : MyBaseObejct {
    protected BaseCombat targetCombat;
    protected Actor targetObject;
    protected Animator CompAnimator;
    
    public virtual void AnimatorInit(Actor target ,BaseCombat combat, Animator animator)
    {
        targetCombat = combat;
        targetObject = target;
        CompAnimator = animator;
    }
    public virtual void AniUpdate()
    {

    }
}
