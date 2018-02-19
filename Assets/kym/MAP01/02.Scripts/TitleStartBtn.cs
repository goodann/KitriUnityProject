using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleStartBtn : MonoBehaviour {

	public void OnClickStartBtn()
    {
        SceneManager.LoadScene("LOBBY", LoadSceneMode.Single);
    }
}
