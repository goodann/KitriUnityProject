using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    private Transform thisTransform = null;
    Transform target = null;

    public float distanceFromTarget = 4.0f;
    public float camHeight = 4.0f;

    //제한치
    public float rotationDamp = 0;
    public float posDamp = 1.0f;

    void Awake()
    {
        thisTransform = GetComponent<Transform>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        //출력속도를 얻는다
        Vector3 velocity = Vector3.zero;

        //회전 보간
        thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, target.rotation, rotationDamp * Time.deltaTime);
        
        //이동 보간
        Vector3 dest = thisTransform.position = Vector3.SmoothDamp(thisTransform.position, target.position,
            ref velocity, posDamp * Time.deltaTime);

        //거리
        thisTransform.position = dest - thisTransform.forward * distanceFromTarget;
        
        //높이
        thisTransform.position = new Vector3(thisTransform.position.x, target.position.y + Mathf.Sqrt(distanceFromTarget)+camHeight, thisTransform.position.z - Mathf.Sqrt(distanceFromTarget));

        
        //Transform.LookAt(_target)
        thisTransform.LookAt(target);
    }
}
