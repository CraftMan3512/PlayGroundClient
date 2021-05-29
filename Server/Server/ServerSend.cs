using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

namespace PlayGroundServer
{
    class ServerSend
    {

        private static void SendTCPData(int toClient, Packet packet)
        {

            packet.WriteLength();
            Server.clients[toClient].tcp.SendData(packet);

        }

        private static void SendUDPData(int toClient, Packet packet)
        {

            packet.WriteLength();
            Server.clients[toClient].udp.SendData(packet);

        }

        private static void SendTCPDataToAll (Packet packet)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {

                Server.clients[i].tcp.SendData(packet);

            }

        }

        private static void SendTCPDataToAll(Packet packet, int[] exceptClients)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (!exceptClients.Contains(i))
                {

                    Server.clients[i].tcp.SendData(packet);

                }

            }

        }

        private static void SendUDPDataToAll(Packet packet)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {

                Server.clients[i].udp.SendData(packet);

            }

        }

        private static void SendUDPDataToAll(Packet packet, int[] exceptClients)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (!exceptClients.Contains(i))
                {

                    Server.clients[i].udp.SendData(packet);

                }

            }

        }

        #region Packets

        public static void Welcome(int toClient, string msg)
        {

            using (Packet packet = new Packet((int)ServerPackets.welcome))
            {

                packet.Write(msg);
                packet.Write(toClient);

                SendTCPData(toClient, packet);

            }

        }

        public static void ToGameScene(int toClient)
        {

            using (Packet packet = new Packet((int)ServerPackets.ToGameScene))
            {

                packet.Write(toClient);
                SendTCPData(toClient, packet);

            }

        }

        public static void SpawnPlayer(int toClient, Player player)
        {

            using (Packet packet = new Packet((int)ServerPackets.SpawnPlayer))
            {

                packet.Write(player.id);
                packet.Write(player.username);
                packet.Write(player.currentScene);

                SendTCPData(toClient, packet);

            };

        }

        public static void PlayerChangedScene(int toClient, int id, int oldScene, int newScene)
        {

            using (Packet packet = new Packet((int) ServerPackets.playerChangeScene))
            {
                
                packet.Write(id);
                packet.Write(oldScene);
                packet.Write(newScene);
                packet.Write(Server.clients[id].player.username);


                SendTCPData(toClient, packet);
                
            };

        }

        public static void PlayerDisconnected (int playerID, int currentScene)
        {

            using (Packet packet = new Packet((int)ServerPackets.playerDisconnected))
            {

                packet.Write(playerID);
                packet.Write(currentScene);

                SendTCPDataToAll(packet);

            };

        }

        public static void PlayerPosition(int toClient, int id, Vector3 position)
        {

            using (Packet packet = new Packet((int)ServerPackets.playerPosition))
            {

                packet.Write(id);
                packet.Write(position);

                SendUDPData(toClient, packet);

            }

        }

        public static void SendMessage(string msg)
        {

            using (Packet packet = new Packet((int)ServerPackets.SendMessage))
            {

                packet.Write(msg);

                SendTCPDataToAll(packet);

            }
        }

        public static void SendMessage(int toClient, string msg)
        {

            using (Packet packet = new Packet((int)ServerPackets.SendMessage))
            {

                packet.Write(msg);

                SendTCPData(toClient, packet);

            }
        }

        public static void ToErrorScreen(int id, string msg)
        {

            using (Packet packet = new Packet((int)ServerPackets.SendMessage))
            {

                packet.Write(msg);

                SendTCPData(id, packet);

            }

        }
        #endregion

    }
}
