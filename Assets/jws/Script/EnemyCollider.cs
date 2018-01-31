using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private Enemy enemy;
    public Enemy Enemy
    {
        get { return enemy; }
        set { enemy = value; }
    }

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
       // enemy.ThrowEvent()   
    }
}
