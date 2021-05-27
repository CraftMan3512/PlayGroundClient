using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneTypes
{
    
    TitleScreen = 0,
    ErrorScreen = 1,
    HubWorldScene = 2,
    
    
}

public static class SceneChanger
{

    private static SceneTypes CurrentScene;
    
    public static void ChangeScene(SceneTypes nextScene)
    {

        //Change scene
        SceneManager.LoadScene((int)nextScene);
        
        //if connected, tell server we changed scene
        if (Client.instance != null)
        {
            
            ClientSend.ChangedScene((int) nextScene);
                
        }
        
        CurrentScene = nextScene;
        

    }

    public static void GoToErrorScreen(string errorMessage)
    {

        ErrorMessageText.currentErrorMessage = errorMessage;
        SceneManager.LoadScene("ErrorScreen");
        //Debug.Log("WENT TO ERROR SCREEN!!!!");

    }

}
