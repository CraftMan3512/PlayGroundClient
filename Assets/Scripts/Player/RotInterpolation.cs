using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotInterpolation : MonoBehaviour
{
     public enum InterpolationTypes
    {
        
        Lerp, Slerp
        
    }
    
    public InterpolationTypes interpolationMode = InterpolationTypes.Lerp;

    
    public int nbPos;
    private float startTime;
    private bool firstRot = true;
    private Vector3 PreviousRot;
    private Queue<Vector3> Movements;
    public Transform tf;

    private void Awake()
    {
        
        startTime = 0.1f;
        Movements = new Queue<Vector3>();
        PreviousRot = tf.eulerAngles;

    }
    private void Update()
    {

        if (Movements.Count > 0)
        {

            double fraction = (Time.time - startTime) / Time.fixedDeltaTime;
            
            //ErrorDisplayer.Log($"TRYING TO MOVE OTHER PLAYER, FRACTION IS {fraction}, {Movements.Count} POS INSIDE");

            if (fraction <= 1f && !firstRot)
            {
                
                switch (interpolationMode)
                {

                    case InterpolationTypes.Lerp:

                        tf.eulerAngles = Quaternion.Lerp(Quaternion.Euler(tf.eulerAngles), Quaternion.Euler(Movements.Peek()), (float) fraction).eulerAngles;
    
                    break;
                    case InterpolationTypes.Slerp:

                        tf.eulerAngles = Quaternion.Slerp(Quaternion.Euler(PreviousRot), Quaternion.Euler(Movements.Peek()), (float) fraction).eulerAngles;
    
                    break;
                    
                }
                
            }
            else
            {

                if (firstRot) firstRot = false;
                PreviousRot = Movements.Dequeue();
                NewRotation();

            }

        }

    }

    //Set time-related variables when changing movement coordinates
    public void NewRotation()
    {
        
        startTime = Time.fixedTime;
        
    }

    public void AddRotation(Vector3 movement)
    {
        
        Movements.Enqueue(movement);
        //ErrorDisplayer.Log($"WE HAVE {Movements.Count} MOVEMENTS IN STOCK!!!"); //TODO Enable this debug to check if the queue risks overloading sometime, seems good for now

    }
}
