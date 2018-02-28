using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Invoke("Destroy1", 0.3f);

	}
    void Destroy1() {
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
