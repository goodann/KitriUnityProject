using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour {

    public float itemInitUpY = 0.5f;
    public float rotationSpeed = 60.0f;

	// Use this for initialization
	void Start ()
    {
        transform.position += new Vector3(0, itemInitUpY, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
	}
}
