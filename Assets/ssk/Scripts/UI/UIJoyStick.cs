﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIJoyStick : MonoBehaviour {
    GameObject Stick;
    float sqrtRadius;
    float timer;
    Vector2 beforeCliickPos;


    const float LimitTime = 1.0f;

    public float Radius;
    public float StickRadius;
    
    public float LimitRadius;
    public float sqrtLimitRadius;

    public static int ClickCount;
    public static Vector2 InputValue;
    // Use this for initialization
    void Start () {
        Stick = GetComponentInChildren<Button>().gameObject;
        Radius = GetComponent<RectTransform>().sizeDelta.x*0.5f;
        StickRadius = GetComponentsInChildren<RectTransform>()[1].sizeDelta.x * 0.5f;
        sqrtRadius = Radius * Radius;
        LimitRadius = Radius - StickRadius;
        sqrtLimitRadius = LimitRadius * LimitRadius;
        InputValue = new Vector2(0, 0);
        timer = 0;
        ClickCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 clickPos= new Vector3(mousePosition.x, mousePosition.y);
        timer += Time.deltaTime;
        if (timer > LimitTime)
        {
            ClickCount = 0;
        }
        if (Input.GetMouseButtonDown(0) && mouseIsInStick(clickPos,2.0f))
        {
            print(beforeCliickPos + "," + mousePosition);
            print("pos diff = "+ (beforeCliickPos - mousePosition).magnitude);
            if ((beforeCliickPos - mousePosition).sqrMagnitude <300)
                ClickCount++;
            else
                ClickCount = 1;
        }
        if (Input.GetMouseButton(0))
        {
            
            OnClick(clickPos);
            beforeCliickPos = mousePosition;
        }
        else
        {
            Vector2 keyInput;
            keyInput.x = Input.GetAxis("Horizontal");
            keyInput.y = Input.GetAxis("Vertical");
            
            OnClick(transform.position + new Vector3(keyInput.x, keyInput.y) * Radius);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Stick.transform.position = transform.position;
            InputValue.x = 0;
            InputValue.y = 0;
            timer = 0;
            
        }
        
    }
    
    public void OnClick(Vector3 clickPos)
    {
        
        //print(mousePosition);
        //if (mouseIsInStick(mousePosition))
        //{
        //Vector3 clickPos = new Vector3(mousePosition.x, mousePosition.y, 0);
        //Stick.transform.position = clickPos;
        //}
        if (mouseIsInStick(clickPos, 2.0f))
        {
             
            Vector3 diff = (clickPos - transform.position);
            if (diff.sqrMagnitude > sqrtLimitRadius)
            {
                diff.Normalize();
                diff *= LimitRadius;
            }
            Stick.transform.position = transform.position + diff;
            InputValue.x = diff.x / Radius;
            InputValue.y = diff.y / Radius;
        }
    }
    bool mouseIsInStick(Vector3 clickPos,float limit=1.0f)
    {
        
        if (sqrtRadius * limit* limit > (transform.position - clickPos).sqrMagnitude)
            return true;
        return false;
    }
}
