using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour {

    [SerializeField]
    float itemPower = 0;
    [SerializeField]
    float itemSpeed = 0;
    [SerializeField]
    float MaxXP;
    [SerializeField]
    float NowXP;
    [SerializeField]
    float useXP = 0;

    public float GetNowXP{ get { return NowXP; } }
    public float GetMaxXP { get { return MaxXP; } }
    public float GetItemPower { get { return itemPower; } }
    public float GetItemSpeed { get { return itemSpeed; } }
    public float GetUseXP { get { return useXP; } }

    void Start()
    {
        NowXP = MaxXP = 100f;

        switch(LayerMask.LayerToName(gameObject.layer))
        {
            case "Mace":
                itemPower = 200;
                itemSpeed = 15;
                useXP = 12;
                break;

            case "Sword":
                itemPower = 150;
                itemSpeed = 30;
                useXP = 8;
                break;

            case "Pistol":
                itemPower = 400;
                itemSpeed = 5;
                useXP = 20;
                break;

            case "Minigun":
                itemPower = 200;
                itemSpeed = 15;
                useXP = 15;
                break;

            case "Shield":
                itemPower = 120;
                itemSpeed = 25;
                useXP = 5;
                break;
        }
    }

    public void UsingItemXP()
    {
        if (NowXP > 0)
            NowXP -= useXP;
        else
            NowXP = 0;

    }

    public void ResetItemState()
    {
        itemPower = 0;
        itemSpeed = 0;
        MaxXP = 0;
        NowXP = 0;
        useXP = 0;
    }
}

