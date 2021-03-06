﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class AttackCollider : MyBaseObejct
{
    GameObject StarParticlePrefab;
    GameObject SkullParticlePrefab;
    GameObject fightAttackParticlePrefab;
    GameObject trail;
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

    // Use this for initialization
    void Start()
    {
        //actor = FindInParentComp<Actor>();
        col = GetComponent<Collider>();
        actor = GetComponentInParent<Actor>();
        StarParticlePrefab = Resources.Load("ssk/prefabs/StarParticle") as GameObject;
        SkullParticlePrefab = Resources.Load("ssk/prefabs/SkullParticle") as GameObject;
        fightAttackParticlePrefab = Resources.Load("ssk/prefabs/FightAttackParticle") as GameObject;
        
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load("ssk/sound/Effect/fight/attack") as AudioClip;
        audioSource.playOnAwake = false;

        trail = new GameObject(this.name + "Trail");
        trail.transform.parent = transform;
        trail.transform.position = transform.position + transform.right * 0.15f;
        trailRenderer = trail.AddComponent<TrailRenderer>();
        trailRenderer.startWidth = 0.2f;
        trailRenderer.endWidth = 0.2f;
        trailRenderer.material = Resources.Load("ssk/Material/AlphaGr3") as Material;
        //trailRenderer.material.SetColor("_TintColor", Color.yellow);
        //trailRenderer.material.SetColor("_TintColor", new Color(0.1f, 1.0f, 1.0f));
        if (gameObject.tag == "Enemy")
        {
            trailRenderer.startColor = Color.red;
            trailRenderer.endColor = Color.black;
        }
        else
        {
            trailRenderer.startColor = new Color(0.1f, 1.0f, 1.0f);
            trailRenderer.endColor = Color.blue;
        }

        //trailRenderer.time = 0.2f;
        trailRenderer.time = 0.5f;

    }
    TrailRenderer trailRenderer;

    // Update is called once per frame
    void Update()
    {
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
        //print("OnTiriggerEnter");
        
        bool isinAttacked = false;
        if (!actor.attackedObject.TryGetValue(other.gameObject, out isinAttacked))
        {
            actor.attackedObject.Add(other.gameObject, true);
        }
        actor.attackedObject[other.gameObject] = true;
        if (isinAttacked == false)
        {
            print("Hit다 hit! 맞은놈 : " + other.ToString() + "데미지 : " + actor.NowPOWER * actor.NowAttackPower + " 밀리는 방향 " + actor.AttackDirction);
            
            
            if (actor.IsUpperAttack)
            {
                other.SendMessage("UpperHit", actor.NowPOWER * actor.NowAttackPower);
            }
            else
            {
                other.SendMessage("onDamaged", actor.NowPOWER * actor.NowAttackPower);
            }
            if (other.CompareTag("Enemy"))
            {
                StageManager.mainPlayer.Behavior.ComboAdd();
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
                StageManager.mainPlayer.AttackCombo();
                //print("Hit다 hit! 맞은놈 : " + other.ToString() + "데미지 : " + actor.NowPOWER * actor.NowAttackPower + " 밀리는 방향 " + actor.AttackDirction);
                //other.SendMessage("onDamaged", actor.NowPOWER * actor.NowAttackPower);

            }
            else if (other.CompareTag("Player"))
            {
                GameObject.Instantiate(SkullParticlePrefab, other.transform.position, Quaternion.Euler(-90, 0, 0));
                GameObject.Instantiate(fightAttackParticlePrefab, other.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                

                
            }
            else
            {

            }

            actor.AttackRecoverMana();
            other.SendMessage("DamagedRecoverMana");
        }
    }


    void ReturnTime()
    {
        isStop = false;
        Time.timeScale = 1.0f;
    }
}
