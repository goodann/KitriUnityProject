using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour {

    public Transform lefthand_MacePos;
    public Transform lefthand_SwordPos;
    public Transform lefthand_MinigunPos;
    public Transform lefthand_ShieldPos;
    public Transform lefthand_PistolPos;

    public bool takeItem;

    private void Start()
    {
        lefthand_MacePos = GameObject.Find("LeftHand_Mace_Pos").transform;
        lefthand_SwordPos = GameObject.Find("LeftHand_Sword_Pos").transform;
        lefthand_MinigunPos = GameObject.Find("LeftHand_Minigun_Pos").transform;
        lefthand_ShieldPos = GameObject.Find("LeftHand_Shield_Pos").transform;
        lefthand_PistolPos = GameObject.Find("LeftHand_Pistol_Pos").transform;

        takeItem = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            if (takeItem) return;

            switch(LayerMask.LayerToName(other.gameObject.layer))
            {
                case "Mace":
                    other.transform.parent = lefthand_MacePos;
                    other.transform.localPosition = new Vector3(0.1f, 0.2f, 0);
                    other.transform.localRotation = Quaternion.Euler(0, 0, 20);
                    break;

                case "Sword":
                    other.transform.parent = lefthand_SwordPos;
                    other.transform.localPosition = new Vector3(-0.04f, 0.4f, -0.25f);
                    other.transform.localRotation = Quaternion.Euler(8, -67, 5);
                    break;

                case "Pistol":
                    other.transform.parent = lefthand_PistolPos;
                    other.transform.localPosition = new Vector3(0.35f, 0, 0.3f);
                    other.transform.localRotation = Quaternion.Euler(0, -17, 0);
                    break;

                case "Minigun":
                    other.transform.parent = lefthand_MinigunPos;
                    other.transform.localPosition = new Vector3(-0.1f, 0.1f, -0.1f);
                    other.transform.localRotation = Quaternion.Euler(30, 0, 50);
                    break;

                case "Shield":
                    other.transform.parent = lefthand_ShieldPos;
                    other.transform.localPosition = new Vector3(0.1f, -0.3f, 0);
                    other.transform.localRotation = Quaternion.identity;
                    break;

            }
            other.isTrigger = false;
            other.gameObject.GetComponent<ItemRotate>().ItemGenePointReset();
            takeItem = true;
        }
    }

}
