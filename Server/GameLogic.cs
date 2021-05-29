using System;
using System.Collections.Generic;
using System.Text;

namespace PlayGroundServer
{
    class GameLogic
    {

        public static void Update()
        {

            foreach (Client client in Server.clients.Values)
            {
                client.player?.Update();

            }

            ThreadManager.UpdateMain();

        }

    }
}
