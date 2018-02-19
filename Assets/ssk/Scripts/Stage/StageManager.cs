using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager:MyBaseObejct{
    public static Light MainLight;
    public static PlayablePlayer mainPlayer;
    public int EnemyCount;
    // Use this for initialization
    void Start () {
        MainLight = GetComponentInChildren<Light>();
        print("Set Light = "+MainLight);
        mainPlayer = GetComponentInChildren<PlayablePlayer>();
        print(mainPlayer);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
