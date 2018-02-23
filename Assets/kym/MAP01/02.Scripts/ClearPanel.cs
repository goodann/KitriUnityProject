using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPanel : MonoBehaviour {

    public GameObject clearBackground = null;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnClickCancleBtn()
    {
        clearBackground.SetActive(false);
    }

    public void OnClickResetBg()
    {
        clearBackground.SetActive(true);
    }
}
