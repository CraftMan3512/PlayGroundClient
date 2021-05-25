using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{

    public CanvasGroup thisMenu;
    public CanvasGroup serversMenu;
    
    public void PlayGame()
    {

        thisMenu.alpha = 0;
        thisMenu.blocksRaycasts = false;
        thisMenu.interactable = false;
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
}