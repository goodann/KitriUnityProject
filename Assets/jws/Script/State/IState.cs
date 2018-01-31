using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IState
{    
    public virtual void Enter(Enemy _parent) { }
    public virtual void Update() { }
    public virtual void Exit() { }
    public virtual IEnumerator CheckMobState() { yield break; }
}
