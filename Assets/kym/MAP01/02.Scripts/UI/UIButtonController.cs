using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour {

    PlayablePlayer playable;

    private void Start()
    {
        playable = GameObject.Find("Player").GetComponent<PlayablePlayer>();
    }

    public void OnPunchBtnDown()
    {
        StartCoroutine("PunchCoroutine");
    }

    public void OnKickBtnDown()
    {
        StartCoroutine("KickCoroutine");
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
