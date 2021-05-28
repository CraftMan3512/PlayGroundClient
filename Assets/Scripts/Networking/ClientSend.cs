using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet packet)
    {
        
        packet.WriteLength();
        Client.instance.tcp.SendData(packet);

    }

    private static void SendUDPData(Packet packet)
    {
        
        packet.WriteLength();
        Client.instance.udp.SendData(packet);
        
    }

    #region Packets

    public static void WelcomeReceived()
    {
        
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {

            packet.Write(Client.GetMyId());
            packet.Write(MenuButtons.usernameEntered);
            //packet.Write(OnlineConnectionManager.instance.passwordField.text); //TODO Hash encrypt password and username
            
            SendTCPData(packet);

        }

    }

    public static void SpawnPlayers()
    {

        using (Packet packet = new Packet((int) ClientPackets.SpawnPlayers))
        {
            
            packet.Write(Client.GetMyId());
            packet.Write((int)SceneChanger.CurrentScene);
            
            SendTCPData(packet);
            
        }
        
    }

    public static void ChangedScene(int newScene)
    {

        using (Packet packet = new Packet((int) ClientPackets.playerChangeScene))
        {
            
         packet.Write(newScene);
         
         SendTCPData(packet);
            
        };

    }

    public static void PlayerPosition(Vector3 newPos)
    {
        
        using (Packet packet = new Packet((int)ClientPackets.playerPosition))
        {
            
            packet.Write(newPos);
            
            SendTCPData(packet);
            
        }
        
    }

    public static void SendMessage(string msg)
    {

        using (Packet packet = new Packet((int) ClientPackets.SendMessage))
        {
            
            packet.Write(msg);
            
            SendTCPData(packet);
            
        }
        
    }

    #endregion
}
