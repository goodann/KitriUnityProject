using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayablePlayer playable;

    void Start()
    {
        playable = GameObject.FindWithTag("Player").GetComponent<PlayablePlayer>();
    }


    //Skill
    public void OnPointerDown(PointerEventData DWevent)
    {
        Debug.Log("isPressed");
        playable.ButtonClick(EButtonList.EBL_Skill);

    }

    public void OnPointerUp(PointerEventData UPevent)
    {
        playable.ButtonRelease(EButtonList.EBL_Skill);
    }

}
