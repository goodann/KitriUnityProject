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
        playable.attack1 = 1;

        yield return new WaitForSeconds(0.3f);
        playable.attack1 = 0;

        yield break;
    }

    IEnumerator KickCoroutine()
    {
        playable.attack2 = 1;

        yield return new WaitForSeconds(0.3f);
        playable.attack2 = 0;

        yield break;
    }

    IEnumerator JumpCoroutine()
    {
        playable.jump = 1;

        yield return new WaitForEndOfFrame();
        playable.jump = 0;

        yield break;
    }

    
}
