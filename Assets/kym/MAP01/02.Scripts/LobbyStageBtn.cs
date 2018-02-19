using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyStageBtn : MonoBehaviour {

    public void OnClickStageBtn()
    {
        SceneManager.LoadScene("STAGE", LoadSceneMode.Single);
    }
}
