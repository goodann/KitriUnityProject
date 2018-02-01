using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackCollider : MyBaseObejct {
    GameObject StarParticlePrefab;
    GameObject fightAttackParticlePrefab;
    AudioSource audioSource;
    Actor actor;
    Dictionary<GameObject, bool> attackedObject;
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
        audioSource.playOnAwake = false;
        attackedObject = new Dictionary<GameObject, bool>();
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
        if (other.CompareTag("Enemy"))
        {
            bool isinAttacked=false;
            if (!attackedObject.TryGetValue(other.gameObject, out isinAttacked))
            {
                attackedObject.Add(other.gameObject, true);
            }
            attackedObject[other.gameObject] = true;
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
                if (attackState == 0)
                {
                    //밀침
                    other.transform.position += (other.transform.position - transform.position) * 0.01f;
                    //onmhit 실행


                    
                }
            }
        }
        print("Hit다 hit! 맞은놈 : " + other.ToString() + "데미지 : " + actor.POWER);
        other.SendMessage("onDamaged", actor.POWER);
    }
    void ReturnTime()
    {
        isStop = false;
        Time.timeScale = 1.0f;
    }
    public void EndAttack()
    {
        List<GameObject> attacked = attackedObject.Keys.ToList<GameObject>();
        for (int i=0; i<attacked.Count; i++)
        {
            attackedObject[attacked[i]] = false;
        }
        
        //print("Attackend!");
    }
}
