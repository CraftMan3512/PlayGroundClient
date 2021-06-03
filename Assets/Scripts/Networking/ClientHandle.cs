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
        
        ErrorDisplayer.Log($"Message from server: {msg}");
        Client.SetId(myId);
        
        ClientSend.WelcomeReceived();
        
        //Actually connect to server
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        
    }

    public static void ToGameScene(Packet packet)
    {

        int id = packet.ReadInt();

        if (id != Client.GetMyId())
        {
            
            ErrorDisplayer.Log("ID MISMATCH!!!!!!!!!");
            
        }
        //change scene
        SceneChanger.ChangeScene(SceneTypes.HubWorldScene, true);
        
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
        int oldScene = packet.ReadInt();
        int newScene = packet.ReadInt();
        string username = packet.ReadString();

        if (SceneChanger.CurrentScene == (SceneTypes) oldScene)
        {

            GameManager.DeletePlayer(id);

        }

        if (SceneChanger.CurrentScene == (SceneTypes) newScene)
        {

            Object.FindObjectOfType<GameManager>().SpawnPlayer(id, username, newScene);

        }

    }
    
    public static void PlayerDisconnected(Packet packet)
    {

        int id = packet.ReadInt();
        int scene = packet.ReadInt();
        if (GameManager.players.ContainsKey(id) && (int)SceneChanger.CurrentScene == scene)
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

    public static void PlayerRotation(Packet packet)
    {

        int id = packet.ReadInt();

        Vector3 newRot = packet.ReadVector3();

        GameManager.UpdatePlayerRotation(id, newRot);

    }

    public static void SendMessage(Packet packet)
    {

        string message = packet.ReadString();
        
        ErrorDisplayer.Log($"Message from server: {message}");

    }

    public static void ToErrorScreen(Packet packet)
    {
        string msg = packet.ReadString();
        Client.instance.Disconnect(new Exception(msg));
        
    }

}
