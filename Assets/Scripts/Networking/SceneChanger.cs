using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public enum SceneTypes
{
    
    TitleScreen = 0,
    ErrorScreen = 1,
    HubWorldScene = 2,
    PlaygroundScene = 3,
    
    
}

public static class SceneChanger
{

    public static SceneTypes CurrentScene;
    
    public static void ChangeScene(SceneTypes nextScene, bool firstLoad = false)
    {
        
        CurrentScene = nextScene;
        //Change scene
        SceneManager.LoadScene((int)nextScene);
        
        //empty players list (local players will be readded in spawnplayers)
        GameManager.players.Clear();

        //if connected, tell server we changed scene
        if (Client.instance != null)
        {
            //if not menu
            if (nextScene != SceneTypes.ErrorScreen && nextScene != SceneTypes.TitleScreen)
            {
                
                ClientSend.ChangedScene((int) nextScene);
                //ClientSend.SpawnPlayers();
                
            }
            else
            {

                Client.instance.CloseConnection();
                //TODO destroy local player here
                DestroyLocalPlayer();

            }
        }
    }

    public static void GoToErrorScreen(string errorMessage)
    {

        ErrorMessageText.currentErrorMessage = errorMessage;
        ThreadManager.ExecuteOnMainThread(() =>
        {
            
            SceneManager.LoadScene((int)SceneTypes.ErrorScreen); 
            DestroyLocalPlayer();
            
        });
        ErrorDisplayer.Log("WENT TO ERROR SCREEN!!!!");

    }

    private static void DestroyLocalPlayer()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");
        if (player != null)
        {
            PlayerControls.exists = false;
            Object.Destroy(player);
                    
        }
        
    }

}
