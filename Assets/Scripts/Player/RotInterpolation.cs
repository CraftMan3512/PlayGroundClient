using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotInterpolation : MonoBehaviour
{
    private float angle;
    private Quaternion nextRot;
    private Transform tf;

    private void Awake()
    {
        
        tf = transform;
        angle = 0;
        nextRot = Quaternion.identity;

    }

    private void Update()
    {

        tf.rotation =
            Quaternion.RotateTowards(tf.rotation, nextRot, angle * (1f / Time.fixedDeltaTime) * Time.deltaTime);

    }

    public void SetRotation(Vector3 rotation)
    {
        
        //ErrorDisplayer.Log($"PLAYER INTERPOL: {movement}");
        angle = Quaternion.Angle(tf.rotation, Quaternion.Euler(rotation));
        nextRot = Quaternion.Euler(rotation);

        //ErrorDisplayer.Log($"WE HAVE {Movements.Count} MOVEMENTS IN STOCK!!!"); //TODO Enable this debug to check if the queue risks overloading sometime, seems good for now

    }
}
