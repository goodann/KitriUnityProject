using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject[] itemObjects;

    public Transform[] points;

    public List<GameObject> itemObjPool = new List<GameObject>();
    GameObject _objectPool;

    public int maxItemCnt;
    public float createTime;

    public bool isGameOver;

    private void Start()
    {
        isGameOver = false;

        points = GetComponentsInChildren<Transform>();
        _objectPool = GameObject.Find("ObjectPool");
        
        for (int i = 0; i < maxItemCnt; i++)
        {
            GameObject item = Instantiate(itemObjects[Random.Range(0, itemObjects.Length)]) as GameObject;
            item.name = item.name + "_" + i.ToString();
            item.SetActive(false);
            itemObjPool.Add(item);
            item.transform.parent = _objectPool.transform;
        }

        StartCoroutine("CreateItem");
    }

    IEnumerator CreateItem()
    {
        while(!isGameOver)
        {
            foreach (GameObject item in itemObjPool)
            {
                if (!item.activeSelf)
                {
                    yield return new WaitForSeconds(createTime);

                    int idx = Random.Range(1, points.Length);

                    //geneOn == false면 건너뜀
                    if (points[idx].GetComponent<ItemGeneInit>().GetGeneOn() == false) continue;

                    item.transform.position = points[idx].position;
                    item.SetActive(true);
                    item.GetComponent<ItemRotate>().itemGenePointIndex = idx;
                    points[idx].GetComponent<ItemGeneInit>().SetGeneOn(false);
                    
                }
            }
        }
        yield return null;
    }
}
