using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_DanceControl : MonoBehaviour {

    Animator playerAnim;

    private void Awake()
    {
        playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    public void OnClickDance1()
    {
        playerAnim.SetTrigger("dance1");
    }

    public void OnClickDance2()
    {
        playerAnim.SetTrigger("dance2");
    }

    public void OnClickDance3()
    {
        playerAnim.SetTrigger("dance3");
    }

    public void OnClickDance4()
    {
        playerAnim.SetTrigger("dance4");
    }

}
