using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    //public GameObject localPlayerCursorPrefab;
    public GameObject playerObjectPrefab;
    
    public void SpawnPlayer(int id, string username, int currentScene)
    {
        
        Debug.Log($"SPawning player #{id}! {username} in scene {currentScene}");
        GameObject player;
        
        if (id == Client.GetMyId()) // si c'est le joueur local
        {
            player = GameObject.FindGameObjectWithTag("LocalPlayer");

        }
        else //si c'est un autre joueur
        {
            
            player = Instantiate(playerObjectPrefab, Vector3.zero, Quaternion.identity);
            
        }

        //Assign values related to player and add it to list
        player.GetComponent<PlayerManager>().id = id;
        player.GetComponent<PlayerManager>().username = username;
        players.Add(id, player.GetComponent<PlayerManager>());
    }

    public static void UpdatePlayerPos(int id, Vector3 newPos)
    {

        if (players.ContainsKey(id)) players[id].GetComponent<PosInterpolation>().AddMovement(newPos);

    }

    public static void UpdatePlayerRotation(int id, Vector3 newRotation)
    {

        if (players.ContainsKey(id)) players[id].GetComponent<RotInterpolation>().AddRotation(newRotation);

    }

    public static void DeletePlayer(int id)
    {

        //delete player object and remove it from players list
        Destroy(players[id].gameObject);
        players.Remove(id);
        
    }
}