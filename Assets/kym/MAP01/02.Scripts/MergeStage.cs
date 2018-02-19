using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeStage : MonoBehaviour {

    private void Awake()
    {
        transform.parent = GameObject.Find("StageManager").transform;
    }
}
