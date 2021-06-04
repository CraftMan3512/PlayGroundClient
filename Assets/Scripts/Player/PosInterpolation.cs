﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PosInterpolation : MonoBehaviour
{


    public int nbPos;
    private float startTime;
    private float dist;
    private Vector3 nextPos;
    private Transform tf;

    private void Awake()
    {
        
        tf = transform;
        startTime = 0.1f;
        dist = 0;
        nextPos = Vector3.zero;

    }

    private void Update()
    {
        
        tf.position = Vector3.MoveTowards(tf.position, nextPos, dist * 30 * Time.deltaTime);
        
    }

    public void AddMovement(Vector3 movement)
    {
        
        //ErrorDisplayer.Log($"PLAYER INTERPOL: {movement}");
        dist = Vector3.Distance(tf.position, nextPos);
        nextPos = movement;

        //ErrorDisplayer.Log($"WE HAVE {Movements.Count} MOVEMENTS IN STOCK!!!"); //TODO Enable this debug to check if the queue risks overloading sometime, seems good for now

    }

}
