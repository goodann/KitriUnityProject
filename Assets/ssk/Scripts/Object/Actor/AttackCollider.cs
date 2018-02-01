using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MyBaseObejct {
    GameObject StarParticlePrefab;
    GameObject fightAttackParticlePrefab;
    AudioSource audioSource;
    Actor actor;
    float timer;
    bool isStop;
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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip= Resources.Load("ssk/sound/Effect/fight/attack") as AudioClip;
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.unscaledDeltaTime;
        if (isStop && timer > 0.05f)
        {
            ReturnTime();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (isStop == false)
        {
            //Time.timeScale = 0.01f;
            //timer = 0;
            Invoke("ReturnTime", 0.05f);
        
            isStop = true;
        }
        print("Hit다 hit!");
        audioSource.Play();
        print(audioSource);
        GameObject.Instantiate(StarParticlePrefab, other.transform.position,Quaternion.Euler(-45,0,0));
        GameObject.Instantiate(fightAttackParticlePrefab, other.transform.position+Vector3.up*0.5f, Quaternion.identity);
        if (attackState == 0)
        {
            //밀침
            other.transform.position += (other.transform.position - transform.position)*0.01f;
            //onmhit 실행

            //print(other);
            other.SendMessage("onDamaged",actor.POWER);
        }
        
    }
    void ReturnTime()
    {
        isStop = false;
        Time.timeScale = 1.0f;
    }
}
