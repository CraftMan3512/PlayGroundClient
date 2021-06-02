using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class vcamSingleton : MonoBehaviour
{
    private static bool exists = false;
    
    // Start is called before the first frame update
    void Awake()
    {

        if (exists) Destroy(gameObject);
        else exists = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
