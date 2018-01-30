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
    public float throwItemSpeed = 100;

    private void Start()
    {
        leftHand_Weapon_Pos = GameObject.Find("LeftHand_Weapon_Pos").transform;

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
        takeItemObj.transform.parent = null;

        itemRigidbody.isKinematic = false;
        itemRigidbody.useGravity = true;
        itemRigidbody.AddForce(takeItemObj.transform.forward * throwItemSpeed);
        isThrowingItemBtnDown = false;
        isGetItemBtnDown = false;
        takeItem = false;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Item")
        {
            //아이템을 줍지 않았을 때
            if (takeItem) return;

            //픽킹가능 상태로 만든다
            isTriggerStaying = true;

            //픽킹버튼을 눌렀을 때
            if (isGetItemBtnDown == false) return;

            //아이템을 줍는다            
            other.transform.parent = leftHand_Weapon_Pos;
            takeItemObj = other.gameObject;
            itemRigidbody = other.GetComponent<Rigidbody>();

            //픽킹 불가능한 상태로 만든다
            isTriggerStaying = false;


            //아이템 종류에 따라 Transform을 조정한다
            switch (LayerMask.LayerToName(other.gameObject.layer))
            {
                
                case "Mace":                    
                    other.transform.localPosition = new Vector3(-0.05f, 0.07f, 0.12f);
                    other.transform.localRotation = Quaternion.Euler(-57, -145, 167);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Sword":
                    other.transform.localPosition = new Vector3(-0.03f, 0.07f, 0.1f);
                    other.transform.localRotation = Quaternion.Euler(12, -60, 114);
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
          

            //아이템 제너레이터 갱신시킨다
            other.gameObject.GetComponent<ItemRotate>().ItemGenePointReset();

            //아이템을 들었다
            takeItem = true;

            //other.isTrigger = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            isTriggerStaying = false;
        }
    }
}
