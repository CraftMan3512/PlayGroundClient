using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class OnEnter : UnityEvent<GameObject>
{
}

public class CollisionZone : MonoBehaviour
{

    public OnEnter onEnter;
    private bool locked = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LocalPlayer" && !locked)
        {
            
            Debug.Log("AGGOGUGUGUGU");
            onEnter.Invoke(other.gameObject);
            locked = true;

        }
        
    }
}