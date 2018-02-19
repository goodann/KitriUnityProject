using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    StageManager stageManager;
    public GameObject[] enemyObjects;

    [SerializeField]
    Transform[] points;

    public List<GameObject> enemyObjPool = new List<GameObject>();
    GameObject _objectPool;
    int maxEnemyCnt;
    public float createTime;

    public bool isGameOver;

    private void Awake()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    private void Start()
    {
        isGameOver = false;

        points = GetComponentsInChildren<Transform>();
        _objectPool = GameObject.Find("ObjectPool");
        maxEnemyCnt = points.Length - 1;
        stageManager.EnemyCount = maxEnemyCnt;

        for(int i=0; i<maxEnemyCnt; i++)
        {
            GameObject enemy = Instantiate(enemyObjects[Random.Range(0,enemyObjects.Length)]) as GameObject;
            enemy.name = enemy.name + "_"+ i.ToString();
            enemy.SetActive(false);
            enemyObjPool.Add(enemy);
            enemy.transform.parent = _objectPool.transform;
        }

        StartCoroutine("CreateEnemy");
    }

    IEnumerator CreateEnemy()
    {
        while(!isGameOver)
        {
            for (int i = 0; i < maxEnemyCnt; i++)
            {
                yield return new WaitForSeconds(createTime);

                GameObject enemy = enemyObjPool[i];

                enemy.transform.position = points[1 + i].position;
                enemy.transform.rotation = points[1 + i].rotation;
                enemy.SetActive(true);

            }
            yield break;
        }
 
        yield return null;
    }
}
