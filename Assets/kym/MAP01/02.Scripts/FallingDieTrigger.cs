using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDieTrigger : MonoBehaviour {

    public GameObject dyingEffect = null;
    GetItem getItem;

    StartPos startPos;

    void Start()
    {
        startPos = GameObject.Find("StartPos").GetComponent<StartPos>();
        getItem = GameObject.FindWithTag("Player").GetComponent<GetItem>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            startPos.SendMessage("ResetPlayerPos", SendMessageOptions.DontRequireReceiver);
            getItem.ResetItemState();
        }

        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Falling die");
            //Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "Item")
        {
            col.gameObject.GetComponent<ItemRotate>().SetupActiveRotation();
        }

        GameObject dyingEff = Instantiate(dyingEffect, col.transform.position, Quaternion.identity) as GameObject;
        Destroy(dyingEff, 1.5f);
    }

}
