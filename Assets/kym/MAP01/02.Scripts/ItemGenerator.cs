using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject[] itemObjects = null;

    public Transform[] points = null;

    public List<GameObject> itemObjPool = new List<GameObject>();
    GameObject _objectPool = null;

    public List<GameObject> effObjPool = new List<GameObject>();

    List<int> indexList = new List<int>();

    public int maxItemCnt = 10;
    int maxPointCnt = 0;
    public float createTime = 5f;

    public bool isGameOver = false;

    public GameObject itemGeneEffect = null;

    private void Start()
    {
        isGameOver = false;

        points = GetComponentsInChildren<Transform>();
        maxPointCnt = points.Length - 1;
        _objectPool = GameObject.Find("ObjectPool");
        
        for (int i = 0; i < maxItemCnt; i++)
        {
            GameObject item = Instantiate(itemObjects[Random.Range(0, itemObjects.Length)]) as GameObject;
            item.name = item.name + "_" + i.ToString();
            item.SetActive(false);
            itemObjPool.Add(item);
            item.transform.parent = _objectPool.transform;
        }

        for(int i = 1; i <= maxPointCnt; i++)
        {
            GameObject geneEff = Instantiate(itemGeneEffect, points[i].position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            geneEff.SetActive(false);
            effObjPool.Add(geneEff);
            geneEff.transform.parent = _objectPool.transform;
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

                    int idx = CheckIndexNumber();  //1~5

                    //foreach (int num in indexList)
                    //    Debug.Log(num);

                    if (idx == 0) continue;

                    //제너레이터 작동중이면 건너뜀
                    if (points[idx].GetComponent<ItemGeneInit>().GetGeneOn() == false) continue;

                    item.transform.position = points[idx].position + new Vector3(0, item.GetComponent<ItemRotate>().itemInitUpY, 0);
                    
                    item.SetActive(true);

                    ItemRotate ir = item.GetComponent<ItemRotate>();
                    ir.itemGenePointIndex = idx;
                    
                    effObjPool[idx-1].SetActive(true);
                    points[idx].GetComponent<ItemGeneInit>().SetGeneOn(false);
                    
                }
            }
        }
        yield return null;
    }

    int CheckIndexNumber()
    {
        int value = 0;

        if (indexList.Count == maxPointCnt)
            return 0;

        while (true)
        {
            int overlap = 0;
            value = Random.Range(1, points.Length);

            foreach(int num in indexList)
            {
                if(value == num)
                {
                    //충돌있음
                    overlap = 1;
                    break;
                }
            }

            if(overlap == 1)
            {
                //충돌있음 -> 다시 검사
                //Debug.Log("Continue");
                continue;
            }
            else
            {
                //충돌없음 -> 탈출
                break;                
            }
            
        }
        indexList.Add(value);
        return value;

    }

    public void RemoveIndexList(int value)
    {
        Debug.Log("Remove value : " + value);
        indexList.Remove(value);
    }
}


