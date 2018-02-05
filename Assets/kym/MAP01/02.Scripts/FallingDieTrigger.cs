using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDieTrigger : MonoBehaviour {

    public GameObject dyingEffect = null;

    StartPos startPos;

    void Start()
    {
        startPos = GameObject.Find("StartPos").GetComponent<StartPos>();
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject dyingEff = Instantiate(dyingEffect, col.transform.position, Quaternion.identity) as GameObject;
        Destroy(dyingEff, 1.5f);

        if (col.gameObject.tag == "Player")
        {
            startPos.SendMessage("ResetPlayerPos", SendMessageOptions.DontRequireReceiver);
        }

        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "Item")
        {
            col.gameObject.GetComponent<ItemRotate>().SetInitRotation();
        }
    }

}
