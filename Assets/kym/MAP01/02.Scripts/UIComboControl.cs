using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComboControl : MonoBehaviour {

    Text comboText;
    Animator comboAnim;
    [SerializeField]
    int comboCnt = 0;

    private void Start()
    {
        comboText = GetComponent<Text>();
        comboAnim = GetComponent<Animator>();
    }

    public void SetComboOnAnim()
    {
        comboAnim.SetBool("ComboOn", true);
    }

    public void SetComboOffAnim()
    {
        if(comboAnim != null)
        {
            comboAnim.SetBool("ComboOn", false);
            
        }
            
    }


    public void SetCombo(int cnt)
    {
        if(comboAnim != null)
        {
            comboCnt = cnt;
            comboText.text = comboCnt + "COMBO!!";
        }
    }
}
