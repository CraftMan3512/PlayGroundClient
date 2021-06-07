using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public CanvasGroup thisMenu;
    public CanvasGroup addServerMenu;
    public CanvasGroup serversMenu;
    
    public static string usernameEntered = "TESTUSERNAME";

    public void SinglePlayer()
    {
        
        SceneChanger.ChangeScene(SceneTypes.HubWorldScene, true);
        
    }
    
    public void PlayGame()
    {

        thisMenu.alpha = 0;
        thisMenu.blocksRaycasts = false;
        thisMenu.interactable = false;
        serversMenu.alpha = 1;
        serversMenu.interactable = true;
        serversMenu.blocksRaycasts = true;

    }

    public void AddServer()
    {
        
        serversMenu.alpha = 0;
        serversMenu.blocksRaycasts = false;
        serversMenu.interactable = false;
        addServerMenu.alpha = 1;
        addServerMenu.interactable = true;
        addServerMenu.blocksRaycasts = true;
        
    }

    public void BackToServers()
    {
        
        addServerMenu.alpha = 0;
        addServerMenu.blocksRaycasts = false;
        addServerMenu.interactable = false;
        serversMenu.alpha = 1;
        serversMenu.interactable = true;
        serversMenu.blocksRaycasts = true;
        
    }

    public void QuitGame()
    {
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        
        
    }

    public void GoToMainMenu()
    {

        SceneChanger.ChangeScene(SceneTypes.TitleScreen);

    }
}