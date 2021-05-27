using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Object = UnityEngine.Object;

public class ClientHandle
{
    public static void Welcome(Packet packet)
    {

        string msg = packet.ReadString();
        int myId = packet.ReadInt();
        
        Debug.Log($"Message from server: {msg}");
        Client.SetId(myId);
        
        ClientSend.WelcomeReceived();
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        
        //change scene
        SceneChanger.ChangeScene(SceneTypes.HubWorldScene);
    }

    public static void SpawnPlayer(Packet packet)
    {

        int id = packet.ReadInt();
        string username = packet.ReadString();
        int currentScene = packet.ReadInt();
        
        Object.FindObjectOfType<GameManager>().SpawnPlayer(id, username, currentScene);

    }

    public static void PlayerChangedScene(Packet packet)
    {

        int id = packet.ReadInt();
        int newScene = packet.ReadInt();
        
        //GameManager.players[id].GetComponent<SceneTracker>().SetCurrentScene(newScene); TODO changer

    }
    
    public static void PlayerDisconnected(Packet packet)
    {

        int id = packet.ReadInt();
        if (GameManager.players.ContainsKey(id))
        {
            
            Object.Destroy(GameManager.players[id].gameObject);
            GameManager.players.Remove(id);   
            
        }

    }

    public static void PlayerPosition(Packet packet)
    {

        int id = packet.ReadInt();

        Vector3 newPos = packet.ReadVector3();
        
        GameManager.UpdatePlayerPos(id, newPos);

    }

    public static void SendMessage(Packet packet)
    {

        string message = packet.ReadString();
        
        Debug.Log($"Message from server: {message}");

    }

    public static void ToErrorScreen(Packet packet)
    {
        string msg = packet.ReadString();
        Client.instance.Disconnect(new Exception(msg));
        
    }

}
