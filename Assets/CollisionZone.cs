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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LocalPlayer")
        {
            
            onEnter.Invoke(other.gameObject);   
            
        }
        
    }
}