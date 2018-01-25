using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneInit : MonoBehaviour {

    public bool geneOn;

    private void Start()
    {
        geneOn = true;
    }


    public bool GetGeneOn()
    {
        return geneOn;
    }

    public void SetGeneOn(bool value)
    {
        geneOn = value;
    }
}
