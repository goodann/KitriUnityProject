using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_BUILDING_Btn : MonoBehaviour {

    public void OnClick_BUILDING_Btn()
    {
        SceneManager.LoadScene("MAP_MODERN_BUILDING", LoadSceneMode.Single);
    }
}
