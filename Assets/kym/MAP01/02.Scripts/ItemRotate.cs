using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour {

    public float itemInitUpY = 0.5f;
    public float rotationSpeed = 120.0f;

    public bool isPickedUp;

    ItemGenerator itemGeneator;
    BoxCollider _boxCollider;
    Rigidbody _rigidbody;
    public int itemGenePointIndex = 0;

    Quaternion initRotation;

    private void Awake()
    {
        initRotation = this.transform.localRotation;
    }

    // Use this for initialization
    void Start ()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        itemGeneator = GameObject.Find("ItemSpawnPoints").GetComponent<ItemGenerator>();
        isPickedUp = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isPickedUp)
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

    }

    public void ItemGenePointReset()
    {
        isPickedUp = true;
        itemGeneator.points[itemGenePointIndex].GetComponent<ItemGeneInit>().SetGeneOn(true);
        itemGeneator.effObjPool[itemGenePointIndex - 1].SetActive(false);
        itemGeneator.RemoveIndexList(itemGenePointIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Stage")
        {
            Invoke("HoldColliderStatement", 2.0f);
        }
    }

    
    void HoldColliderStatement()
    {
        _boxCollider.isTrigger = true;
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
    }

    public void SetupActiveRotation()
    {
        HoldColliderStatement();
        transform.rotation = initRotation;
        itemGenePointIndex = 0;
        isPickedUp = false;
        gameObject.SetActive(false);
        
    }
}
