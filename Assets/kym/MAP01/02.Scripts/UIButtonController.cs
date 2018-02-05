using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour {

    PlayablePlayer playable;
    GetItem getItem;

    private void Start()
    {
        playable = GameObject.FindWithTag("Player").GetComponent<PlayablePlayer>();
        getItem = GameObject.FindWithTag("Player").GetComponent<GetItem>();
    }

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

    public void OnJumpBtnDown()
    {
        StartCoroutine("JumpCoroutine");
    }

    IEnumerator PunchCoroutine()
    {
        playable.ButtonClick(EButtonList.EBL_AttackA);

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
