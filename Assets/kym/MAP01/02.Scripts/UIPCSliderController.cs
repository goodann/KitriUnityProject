using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPCSliderController : MonoBehaviour {

    Slider pcHpSlider;
    Slider pcMpSlider;
    Slider pcXpSlider;
    Slider pcSpSlider;

    PlayablePlayer playable;

    private void Awake()
    {
        playable = GameObject.Find("Player").GetComponent<PlayablePlayer>();
    }

    private void OnEnable()
    {
        pcHpSlider = GameObject.Find("PC_HPbar").GetComponent<Slider>();
        pcMpSlider = GameObject.Find("PC_MPbar").GetComponent<Slider>();
        pcXpSlider = GameObject.Find("PC_XPbar").GetComponent<Slider>();
        pcSpSlider = GameObject.Find("PC_SPbar").GetComponent<Slider>();
    }

    private void Update()
    {
        UpdatePlayerInfo();
    }

    void UpdatePlayerInfo()
    {
        pcHpSlider.maxValue = playable.HP;
        pcHpSlider.value = playable.NowHP;

        pcMpSlider.maxValue = playable.MP;
        pcMpSlider.value = playable.NowMP;

        //pcSpSlider.maxValue = 300f;
        //pcSpSlider.value = playable.SkillCharged;
    }

    public void GetItemState(float maxXP, float nowXP)
    {
        pcXpSlider.maxValue = maxXP;
        pcXpSlider.value = nowXP;
    }

}
