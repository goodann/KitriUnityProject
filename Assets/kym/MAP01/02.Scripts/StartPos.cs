using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour {

    public GameObject playerObject = null;

    public Transform startPosition = null;

    void Start()
    {
        ResetPlayerPos();
    }

    void ResetPlayerPos()
    {
        playerObject.transform.position = startPosition.position + new Vector3(0, 0.1f, 0);
        playerObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    
}
