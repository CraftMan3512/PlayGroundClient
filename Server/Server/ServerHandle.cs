using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlayGroundServer
{
    class ServerHandle
    {

        public static void WelcomeReceived (int fromClient, Packet packet)
        {

            int clientIdCheck = packet.ReadInt();
            string username = packet.ReadString();
            
            //Connection successful
            Console.WriteLine($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully as player #{fromClient} and username {username}.");

            if (Server.clients[fromClient].CanConnect(username)) Server.clients[fromClient].SendIntoGame(username);

        }

        public static void PlayerChangedScene(int fromClient, Packet packet)
        {

            //unpack packet
            int newScene = packet.ReadInt();
            int oldScene = Server.clients[fromClient].player.currentScene;

            //set new scene for player
            Console.WriteLine($"PLAYER ID {fromClient} CHANGED FROM SCENE {oldScene} TO {newScene}!");
            Server.clients[fromClient].player.SetCurrentScene(newScene);

            //send change scene packet to all players that have to either delete the moving player or spawn it
            foreach (var c in Server.clients.Values)
            {

                if (c.player != null && c.id != fromClient)
                {

                    //si c est a l'ancienne scene ou la nouvelle scene
                    if (c.player.currentScene == newScene || c.player.currentScene == oldScene)
                    {

                        ServerSend.PlayerChangedScene(c.id, fromClient, oldScene, newScene);

                    }

                }

            }

            //spawn players for new scene
            Server.clients[fromClient].SpawnPlayers(newScene);

            //Program.saveData.DisplayPlayersInfo();

        }

        public static void PlayerPosition(int fromClient, Packet packet)
        {

            Vector3 newPos = packet.ReadVector3();

            if (Server.clients[fromClient].player != null)
            {

                Server.clients[fromClient].player.posX = newPos.X;
                Server.clients[fromClient].player.posY = newPos.Y;
                Server.clients[fromClient].player.posZ = newPos.Z;

            }

            //send new pos to all players in the same scene
            foreach (var c in Server.clients.Values)
            {

                if (c.player != null && Server.clients[fromClient].player != null)
                {

                    if (c.player.currentScene == Server.clients[fromClient].player.currentScene && c.id != fromClient)
                    {

                        ServerSend.PlayerPosition(c.id, fromClient, newPos);

                    }

                }

            }

        }

        public static void SendMessage(int fromClient, Packet packet)
        {

            string msg = packet.ReadString();

            Console.WriteLine($"Message from client #{fromClient}: {msg}");

        }

        public static void SpawnPlayers(int fromClient, Packet packet)
        {

            int id = packet.ReadInt();
            int currentScene = packet.ReadInt();

            Server.clients[fromClient].SpawnPlayers(currentScene);

        }

    }
}
