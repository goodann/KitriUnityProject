using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour {

    Transform leftHand_Weapon_Pos;

    public bool takeItem;
    public bool isGetItemBtnDown = false;
    public bool isTriggerStaying = false;

    GameObject takeItemObj = null;
    public bool isThrowingItemBtnDown = false;

    Rigidbody itemRigidbody;
    public float throwItemSpeed = 10000;
    public float throwPower = 8000;

    Animator playerAnimator;

    UIPCSliderController pcXPController;

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

    public float GetNowXP { get { return NowXP; } set { NowXP = value; } }
    public float GetMaxXP { get { return MaxXP; } }
    public float GetItemPower { get { return itemPower; } }
    public float GetItemSpeed { get { return itemSpeed; } }
    public float GetUseXP { get { return useXP; } }


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        leftHand_Weapon_Pos = GameObject.Find("LeftHand_Weapon_Pos").transform;
        pcXPController = GameObject.Find("PCPanel").GetComponent<UIPCSliderController>();

        takeItem = false;

    }

    private void Update()
    {
        if (takeItemObj == null) return;
        if (itemRigidbody == null) return;
        if (isThrowingItemBtnDown == false) return;

        ThrowObject();
    }

    void ThrowObject()
    {
        //상태 초기화
        itemRigidbody.isKinematic = false;
        itemRigidbody.useGravity = true;
        isTriggerStaying = false;
        itemRigidbody.freezeRotation = false;
        takeItemObj.GetComponent<BoxCollider>().isTrigger = false;

        //아이템을 날린다
        playerAnimator.SetTrigger("Throwing");
        itemRigidbody.AddForce(transform.forward * throwItemSpeed);
        itemRigidbody.AddTorque(transform.forward * throwPower);

        //아이템 정보 초기화
        ResetItemState();
    }




    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Item")
        {
            //아이템 정보를 저장
            takeItemObj = other.gameObject;
            itemRigidbody = takeItemObj.GetComponent<Rigidbody>();

            itemRigidbody.isKinematic = true;
            itemRigidbody.useGravity = false;
            itemRigidbody.freezeRotation = true;


            //아이템을 주운 상태면 리턴
            if (takeItem) return;

            //픽킹가능 상태로 만든다
            isTriggerStaying = true;

            //픽킹버튼을 눌렀을 때
            if (isGetItemBtnDown == false) return;

            //아이템을 줍는다(부모연결)
            takeItemObj.transform.parent = leftHand_Weapon_Pos;
            playerAnimator.SetTrigger("Picking");

            //아이템 정보를 얻는다 //아이템 정보를 보낸다
            GetItemState();

            //아이템을 들었다
            takeItem = true;

            //픽킹 불가능한 상태로 만든다
            isTriggerStaying = false;


            //아이템 종류에 따라 Transform을 조정한다
            switch (LayerMask.LayerToName(other.gameObject.layer))
            {
                
                case "Mace":                    
                    other.transform.localPosition = new Vector3(-0.05f, 0.07f, 0.12f);
                    other.transform.localRotation = Quaternion.Euler(-57, -150, 170);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Sword":
                    other.transform.localPosition = new Vector3(-0.03f, 0.07f, 0.1f);
                    other.transform.localRotation = Quaternion.Euler(-32, -60, 114);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Pistol":
                    other.transform.localPosition = new Vector3(-0.28f, -0.17f, 0.05f);
                    other.transform.localRotation = Quaternion.Euler(118, 253, 59);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Minigun":
                    other.transform.localPosition = new Vector3(-0.19f, -0.07f, 0.1f);
                    other.transform.localRotation = Quaternion.Euler(150, 113, -61);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Shield":
                    other.transform.localPosition = new Vector3(-0.11f, 0.1f, 0.21f);
                    other.transform.localRotation = Quaternion.Euler(56, 58, -130);
                    other.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    break;

            }

            //다시 주운 아이템이면 제너레이터 작동 패스
            if (takeItemObj.GetComponent<ItemRotate>().isPickedUp) return;

            //아이템 제너레이터를 갱신시킨다
            other.gameObject.GetComponent<ItemRotate>().ItemGenePointReset();

        }
    }

    public void GetItemState()
    {
        if (takeItemObj == null) return;

        itemPower = takeItemObj.GetComponent<ItemState>().GetItemPower;
        itemSpeed = takeItemObj.GetComponent<ItemState>().GetItemSpeed;
        MaxXP = takeItemObj.GetComponent<ItemState>().GetMaxXP;
        NowXP = takeItemObj.GetComponent<ItemState>().GetNowXP;
        useXP = takeItemObj.GetComponent<ItemState>().GetUseXP;

        SetItemState();
    }

    void SetItemState()
    {
        pcXPController.GetItemState(MaxXP, NowXP);
    }

    public void UsingItemXP()
    {
        if (takeItemObj == null) return;

        if(NowXP <= useXP)
        {
            SetupActiveRotation();
            ResetItemState();
            return;
        }

        takeItemObj.GetComponent<ItemState>().UsingItemXP();
    }


    public void ResetItemState()
    {
        if (takeItemObj == null) return;

        itemPower = 0;
        itemSpeed = 0;
        MaxXP = 0;
        NowXP = 0;
        useXP = 0;

        SetItemState();

        //부모연결을 끊는다 -> 오브젝트풀로 리턴
        takeItemObj.transform.parent = GameObject.Find("ObjectPool").transform;

        //정보리셋
        takeItemObj = null;
        itemRigidbody = null;

        //상태처리
        isThrowingItemBtnDown = false;
        isGetItemBtnDown = false;
        takeItem = false;
    }

    void SetupActiveRotation()
    {
        if (takeItemObj == null) return;

        takeItemObj.GetComponent<ItemRotate>().SetupActiveRotation();
    }
}
