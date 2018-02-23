using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour {

    public GameObject optionPanel = null;

    public void OnClickOptionBtn()
    {
        optionPanel.SetActive(true);
    }
}
