using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MyBaseObejct {
    GameObject StarParticlePrefab;
    GameObject fightAttackParticlePrefab;
    Actor actor;
    public int attackState;
    public int AttackState
    {
        get { return attackState; }
        set { attackState = value; }
    }
	// Use this for initialization
	void Start () {
        //actor = FindInParentComp<Actor>();
        actor = GetComponentInParent<Actor>();
        StarParticlePrefab = Resources.Load("ssk/prefabs/StarParticle") as GameObject;
        fightAttackParticlePrefab = Resources.Load("ssk/prefabs/FightAttackParticle") as GameObject;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        print("Hit!");
        GameObject.Instantiate(StarParticlePrefab, other.transform.position,Quaternion.Euler(-45,0,0));
        GameObject.Instantiate(fightAttackParticlePrefab, other.transform.position+Vector3.up*0.5f, Quaternion.identity);
        if (attackState == 0)
        {
            //밀침
            other.transform.position += (other.transform.position - transform.position)*0.01f;
            //onmhit 실행

            other.SendMessage("onDamaged",actor.POWER);
        }
        
    }
}
