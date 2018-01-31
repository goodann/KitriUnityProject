using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager:MyBaseObejct{
    public static Light MainLight;
	// Use this for initialization
	void Start () {
        MainLight = GetComponentInChildren<Light>();
        print("Set Light = "+MainLight);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
