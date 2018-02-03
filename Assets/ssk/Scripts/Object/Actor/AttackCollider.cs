using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class AttackCollider : MyBaseObejct {
    GameObject StarParticlePrefab;
    GameObject fightAttackParticlePrefab;
    AudioSource audioSource;
    Actor actor;
    Collider col;
    float timer;
    bool isStop;
    
    public int attackState;
    public int AttackState
    {
        get { return attackState; }
        set { attackState = value; }
    }
    int i = 0;
	// Use this for initialization
	void Start () {
        //actor = FindInParentComp<Actor>();
        col = GetComponent<Collider>();
        actor = GetComponentInParent<Actor>();
        StarParticlePrefab = Resources.Load("ssk/prefabs/StarParticle") as GameObject;
        fightAttackParticlePrefab = Resources.Load("ssk/prefabs/FightAttackParticle") as GameObject;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip= Resources.Load("ssk/sound/Effect/fight/attack") as AudioClip;
        audioSource.playOnAwake = false;


        trailRenderer = gameObject.AddComponent<TrailRenderer>();
        trailRenderer.startWidth = 0.2f;
        trailRenderer.endWidth = 0.2f;
        trailRenderer.material = Resources.Load("ssk/Material/AlphaGr3") as Material;
        //trailRenderer.material.SetColor("_TintColor", Color.yellow);
        //trailRenderer.material.SetColor("_TintColor", new Color(0.1f, 1.0f, 1.0f));
        trailRenderer.startColor = Color.red;
        trailRenderer.endColor = new Color(0.1f, 1.0f, 1.0f);

        //trailRenderer.time = 0.2f;
        trailRenderer.time = 0.5f;

    }
    TrailRenderer trailRenderer;
    
    // Update is called once per frame
    void Update () {
        if (col.enabled == true)
        {
            trailRenderer.enabled = true;
            trailRenderer.minVertexDistance = 0.1f;
        }
        else
        {
            trailRenderer.enabled = false;
            //Invoke("TrailDisable", 0.5f);
        }
        timer += Time.unscaledDeltaTime;
        if (isStop && timer > 0.05f)
        {
            ReturnTime();
        }
    }
    void TrailDisable()
    {
        trailRenderer.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        print("Hit다 hit! 맞은놈 : " + other.ToString() + "데미지 : " + actor.NowPOWER * actor.NowAttackPower + " 밀리는 방향 " + actor.AttackDirction);
        other.SendMessage("onDamaged", actor.NowPOWER * actor.NowAttackPower);
        if (other.CompareTag("Enemy"))
        {
            bool isinAttacked=false;
            if (! actor.attackedObject.TryGetValue(other.gameObject, out isinAttacked))
            {
                actor.attackedObject.Add(other.gameObject, true);
            }
            actor.attackedObject[other.gameObject] = true;
            if (isinAttacked == false)
            {
                if (isStop == false)
                {
                    //Time.timeScale = 0.01f;
                    //timer = 0;
                    Invoke("ReturnTime", 0.1f);

                    isStop = true;
                }
                
                audioSource.Play();
                //print(audioSource);
                GameObject.Instantiate(StarParticlePrefab, other.transform.position, Quaternion.Euler(-45, 0, 0));
                GameObject.Instantiate(fightAttackParticlePrefab, other.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                other.transform.position += actor.AttackDirction;
                //print("Hit다 hit! 맞은놈 : " + other.ToString() + "데미지 : " + actor.NowPOWER * actor.NowAttackPower + " 밀리는 방향 " + actor.AttackDirction);
                //other.SendMessage("onDamaged", actor.NowPOWER * actor.NowAttackPower);

            }
        }
        else
        {
            
        }
        

    }
    void ReturnTime()
    {
        isStop = false;
        Time.timeScale = 1.0f;
    }
}
