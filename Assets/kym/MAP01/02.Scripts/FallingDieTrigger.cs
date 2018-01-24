using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDieTrigger : MonoBehaviour {

    StartPos startPos;
    void Start()
    {
        startPos = GameObject.Find("StartPos").GetComponent<StartPos>();
    }

	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("trigger on");
            startPos.SendMessage("ResetPlayerPos", SendMessageOptions.DontRequireReceiver);
        }
    }
}
