using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIButtonController : MonoBehaviour
{

    PlayablePlayer playable;
    GetItem getItem;
    bool skillBtnPressed = false;

    private void Start()
    {
        playable = GameObject.FindWithTag("Player").GetComponent<PlayablePlayer>();
        getItem = GameObject.FindWithTag("Player").GetComponent<GetItem>();
    }

    private void Update()
    {
        if(skillBtnPressed)
        {
            Debug.Log("isPressed");
            playable.ButtonClick(EButtonList.EBL_Skill);
        }
    }

    //펀치 버튼
    public void OnPunchBtnDown()
    {
        if (getItem.isTriggerStaying && getItem.takeItem == false)
            getItem.isGetItemBtnDown = true;
        else
        {
            getItem.isGetItemBtnDown = false;
            StartCoroutine("PunchCoroutine");
        }       
    }

    //킥 버튼
    public void OnKickBtnDown()
    {
        if (getItem.takeItem)
            getItem.isThrowingItemBtnDown = true;
        else
        {
            getItem.isThrowingItemBtnDown = false;
            StartCoroutine("KickCoroutine");
        }

    }

    //점프 버튼
    public void OnJumpBtnDown()
    {
        StartCoroutine("JumpCoroutine");
    }

    //스킬 버튼 누름
    public void OnSkillBtnDown()
    {
        skillBtnPressed = true;
    }

    //스킬 버튼 뗌
    public void OnSkillBtnUp()
    {
        skillBtnPressed = false;
        playable.ButtonRelease(EButtonList.EBL_Skill);
    }


    IEnumerator PunchCoroutine()
    {
        playable.ButtonClick(EButtonList.EBL_AttackA);

        if (getItem.takeItem == true)
        {
            getItem.UsingItemXP();
            getItem.GetItemState();
        }

        yield return new WaitForSeconds(0.3f);
        playable.ButtonRelease(EButtonList.EBL_AttackA);

        yield break;
    }

    IEnumerator KickCoroutine()
    {
        playable.ButtonClick(EButtonList.EBL_AttackB);

        yield return new WaitForSeconds(0.3f);
        playable.ButtonRelease(EButtonList.EBL_AttackB);

        yield break;
    }

    IEnumerator JumpCoroutine()
    {
        playable.ButtonClick(EButtonList.EBL_Jump);

        yield return new WaitForEndOfFrame();
        playable.ButtonRelease(EButtonList.EBL_Jump);

        yield break;
    }


}
