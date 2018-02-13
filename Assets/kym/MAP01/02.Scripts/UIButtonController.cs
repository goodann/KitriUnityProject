using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIButtonController : MonoBehaviour
{

    PlayablePlayer playable;
    GetItem getItem;
    bool skillBtnPressed = false;
    [SerializeField]
    bool isAttacking = false;

    Image Attack_Arms_Converter;

    public Sprite PunchSprite = null;
    public Sprite ArmsSprite = null;

    private void Start()
    {
        playable = GameObject.FindWithTag("Player").GetComponent<PlayablePlayer>();
        getItem = GameObject.FindWithTag("Player").GetComponent<GetItem>();
        Attack_Arms_Converter = transform.Find("PunchBtn").GetComponent<Image>();
        Attack_Arms_Converter.sprite = PunchSprite;
    }

    private void Update()
    {
        if(skillBtnPressed)
        {
            //Debug.Log("isPressed");
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
            //StartCoroutine("PunchCoroutine");

            if(isAttacking == false)
            {
                playable.ButtonClick(EButtonList.EBL_AttackA);
                isAttacking = true;

                if (getItem.takeItem == true)
                {
                    getItem.UsingItemXP();
                    getItem.GetItemState();
                }
                Invoke("PunchOff", 0.6f);
            }
            
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


    void PunchOff()
    {
        playable.ButtonRelease(EButtonList.EBL_AttackA);
        isAttacking = false;
    }

    //IEnumerator PunchCoroutine()
    //{
    //    playable.ButtonClick(EButtonList.EBL_AttackA);

    //    if (getItem.takeItem == true && isAttacking == false)
    //    {
    //        getItem.UsingItemXP();
    //        getItem.GetItemState();
    //        isAttacking = true;
    //    }

    //    yield return new WaitForSeconds(0.6f);
    //    playable.ButtonRelease(EButtonList.EBL_AttackA);
    //    isAttacking = false;

    //    yield break;
    //}

    IEnumerator KickCoroutine()
    {
        playable.ButtonClick(EButtonList.EBL_AttackB);

        yield return new WaitForSeconds(0.6f);
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


    public void ConvertAttackBtnImages()
    {
        if (getItem.takeItem)
            Attack_Arms_Converter.sprite = ArmsSprite;
        else
            Attack_Arms_Converter.sprite = PunchSprite;
    }

}
