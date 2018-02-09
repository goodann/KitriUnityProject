using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UINPCSliderController : MonoBehaviour {

    EnemyManager enemyMgr;
    Slider npcHpSlider;
    Image[] images;

    public bool isActive = false;

    private void Awake()
    {
        enemyMgr = GameObject.Find("StageManager").GetComponent<EnemyManager>();
    }

    private void OnEnable()
    {
        npcHpSlider = GameObject.Find("NPC_HPbar").GetComponent<Slider>();
        images = GetComponentsInChildren<Image>();

        HideLastHitMobInfo();
    }

    private void Update()
    {
        if(enemyMgr.LastHitMobMaxHP != 0 && enemyMgr.LastHitMobHP != 0 && isActive == false)
            ActiveLastHitMobInfo();

        if (enemyMgr.LastHitMobMaxHP == 0 && enemyMgr.LastHitMobHP == 0 && isActive == true)
            HideLastHitMobInfo();

        UpdateLastHitMobInfo();
    }

    
    void UpdateLastHitMobInfo()
    {
        npcHpSlider.maxValue = enemyMgr.LastHitMobMaxHP;
        npcHpSlider.value = enemyMgr.LastHitMobHP;
    }

    void ActiveLastHitMobInfo()
    {
        foreach (Image ims in images)
            ims.enabled = true;

        isActive = true;

        StartCoroutine("CheckHitTimer");
    }

    void HideLastHitMobInfo()
    {
        foreach (Image ims in images)
            ims.enabled = false;

        isActive = false;
    }



    IEnumerator CheckHitTimer()
    {
        while(true)
        {
            float currHP = enemyMgr.LastHitMobHP;

            yield return new WaitForSeconds(2.0f);

            if(currHP == enemyMgr.LastHitMobHP)
            {
                enemyMgr.ResetMobInfo();
                HideLastHitMobInfo();
                yield break;
            }
        }
    }
}
