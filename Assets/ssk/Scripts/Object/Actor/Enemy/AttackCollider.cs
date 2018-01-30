using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {

    public int attackState;
    public int AttackState
    {
        get { return attackState; }
        set { attackState = value; }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        print("Hit!");
        
        if (attackState == 0)
        {
            other.transform.position += (other.transform.position - transform.position)*0.1f;
        }
        
    }
}
