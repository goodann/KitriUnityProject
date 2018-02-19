using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_SCI_FI_Btn : MonoBehaviour {

	public void OnClick_SCI_FI_Btn()
    {
        SceneManager.LoadScene("MAP_SCI_FI_CITY", LoadSceneMode.Single);
    }
}
