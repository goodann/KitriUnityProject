using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager:MyBaseObejct{
    public static Light MainLight;
    public static PlayablePlayer mainPlayer;
    public int EnemyCount;
    public GameObject clearPanel = null;
    // Use this for initialization
    void Start () {
        MainLight = GetComponentInChildren<Light>();
        print("Set Light = "+MainLight);
        mainPlayer = GetComponentInChildren<PlayablePlayer>();
        print(mainPlayer);

        StartCoroutine("ClearCheckCoroutine");
    }


    IEnumerator ClearCheckCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);

            if(EnemyCount <= 0)
            {
                clearPanel.SetActive(true);

                Debug.Log("GameOver~!!!!!!!!!!!!!!!!!!!!!!!!!");
                StopAllCoroutines();
                yield break;
            }
        }
    }
}
