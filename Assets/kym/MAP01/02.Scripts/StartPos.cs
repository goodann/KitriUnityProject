using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour {

    GameObject playerObject;
    Transform startPosition;

    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
        startPosition = this.transform;
    }

    void Start()
    {
        ResetPlayerPos();
    }

    void ResetPlayerPos()
    {
        playerObject.transform.position = startPosition.position + new Vector3(0, 0.1f, 0);
        playerObject.transform.rotation = Quaternion.identity;
    }

    void HoldPlayerMove()
    {
        
    }
    
}
