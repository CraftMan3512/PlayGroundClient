using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ErrorDisplayer : MonoBehaviour
{
    private static bool debugMode = false;

    public enum ErrorTypes
    {
        
        Info, Warning, Error
        
    }
    
    public TextMeshProUGUI display;

    private static ErrorDisplayer instance;

    private void Awake()
    {

        if (debugMode)
        {
            
            if (instance != null)
            {
            
                Destroy(transform.parent.parent.gameObject);
            
            }
            else
            {

                instance = this;

            }

        }
        else
        {
            
            Destroy(transform.parent.parent.gameObject);
            
        }
    }

    public static void Log(string text, ErrorTypes errorType = ErrorTypes.Info)
    {

        
        switch (errorType)
        {
            
            case ErrorTypes.Info:

                Debug.Log(text);
                
                break;
            case ErrorTypes.Warning:

                Debug.LogWarning(text);
                
                break;
            case ErrorTypes.Error:

                Debug.LogError(text);
                break;
            
        }

        if (instance != null && debugMode)
        {
            
            if (instance.display != null)
            {

                List<string> msgs = instance.display.text.Split('\n').ToList();
                
                if (msgs.Count > 10)
                {

                    msgs = msgs.Take(10).ToList();
                    msgs.Insert(9,"\n");
                    msgs.Insert(8,"\n");
                    msgs.Insert(7,"\n");
                    msgs.Insert(6,"\n");
                    msgs.Insert(5,"\n");
                    msgs.Insert(4,"\n");
                    msgs.Insert(3,"\n");
                    msgs.Insert(2,"\n");
                    msgs.Insert(1,"\n");
                    msgs.Insert(0,"\n");
                    instance.display.text = String.Concat(text, String.Concat(msgs));
                    

                }
                else
                {

                    instance.display.text = String.Concat(text, "\n", instance.display.text);

                }  
            
            }   
            
        }

    }
    
}
