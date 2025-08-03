using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragCollider : MonoBehaviour
{
    dragObject mObject;
    // Start is called before the first frame update
    void Start()
    {
        mObject = GetComponentInParent<dragObject>();
    }

    private void OnMouseDown()
    {
        Debug.Log("dragStarted");
        mObject.startDrag();
    }

    private void OnMouseUp() 
    {
        mObject.stopDrag();
    
    }
}
