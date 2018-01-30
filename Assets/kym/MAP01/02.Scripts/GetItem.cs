using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour {

    Transform leftHand_Weapon_Pos;

    public bool takeItem;

    private void Start()
    {
        leftHand_Weapon_Pos = GameObject.Find("LeftHand_Weapon_Pos").transform;

        takeItem = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            if (takeItem) return;

            other.transform.parent = leftHand_Weapon_Pos;

            switch (LayerMask.LayerToName(other.gameObject.layer))
            {
                
                case "Mace":                    
                    other.transform.localPosition = new Vector3(-0.048f, 0.069f, 0.124f);
                    other.transform.localRotation = Quaternion.Euler(-57, -145, 167);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Sword":
                    other.transform.localPosition = new Vector3(-0.029f, 0.075f, 0.106f);
                    other.transform.localRotation = Quaternion.Euler(12, -60, 114);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Pistol":
                    other.transform.localPosition = new Vector3(-0.281f, -0.169f, 0.054f);
                    other.transform.localRotation = Quaternion.Euler(118, 253, 59);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Minigun":
                    other.transform.localPosition = new Vector3(-0.189f, -0.075f, 0.098f);
                    other.transform.localRotation = Quaternion.Euler(150, 113, -61);
                    other.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;

                case "Shield":
                    other.transform.localPosition = new Vector3(-0.11f, 0.108f, 0.213f);
                    other.transform.localRotation = Quaternion.Euler(56, 58, -130);
                    other.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    break;

            }
            other.isTrigger = false;
            other.gameObject.GetComponent<ItemRotate>().ItemGenePointReset();
            takeItem = true;
        }
    }

}
