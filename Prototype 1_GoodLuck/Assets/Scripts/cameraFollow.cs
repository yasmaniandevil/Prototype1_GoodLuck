using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    //object we want camera to follow
    public Transform targetObject;
    //distance btwn then
    private Vector3 initalOffset;
    //current cam position
    private Vector3 CameraPos;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //distance between them is current position minus target object position
        initalOffset = transform.position - targetObject.position;
        //Debug.Log("inital offset" + initalOffset);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //the position of the camera is equal to the sum of the target 
        //obj position and the offset
        CameraPos = targetObject.position + initalOffset;
        //Debug.Log("CameraPos" + CameraPos);
        //transform the position of the camera based off of above
        transform.position = CameraPos;
    }
}
