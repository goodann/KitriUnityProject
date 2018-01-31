using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEquipmentState
{
    CharEqState_Fight = 0,
    CharEqState_Sword = 1,
}
public enum ECharaterState
{
    CharState_idle,
    CharState_move,
    CharState_attack,
    CharState_dead,
}

public enum EAttackColliderIndex
{
    ACI_LeftFoot = 0,
    ACI_RightFoot = 1,
    ACI_LeftHand = 2,
    ACI_RightHand = 3,
}
public enum EBaseObjectState
{
    ObjectState_Normal,
    ObjectState_Die,
}

public enum EButtonList
{
    EBL_AttackA,
    EBL_AttackB,
    EBL_Skill,
    EBL_Jump,
}