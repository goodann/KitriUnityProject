using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject[] itemObjects;

    [SerializeField]
    Transform[] points;

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

                    bool geneOn = points[idx].GetComponent<ItemGeneInit>().geneOn;

                    if (!geneOn) continue;

                    item.transform.position = points[idx].position + new Vector3(0, 0.5f, 0);
                    item.SetActive(true);
                    points[idx].GetComponent<ItemGeneInit>().geneOn = false;
                }
            }
        }
        yield return null;
    }
}
