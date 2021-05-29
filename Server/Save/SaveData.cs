using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace PlayGroundServer
{
    public class SaveData
    {

        public string savePath = "Save.sav";

        public DateTime lastSaved = DateTime.Now;
        
        //Username, Password
        public Dictionary<string, Player> users;

        public SaveData () {}
        
        #region FileSaving
        public void SaveGameData()
        {
            
            FileStream stream = new FileStream(savePath, FileMode.Create);
            
            try
            {

                //setupping formatter for binary save
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //saving user credentials
                binaryFormatter.Serialize(stream, users);

                lastSaved = DateTime.Now;

            }
            catch (Exception e)
            {

                Console.WriteLine($"Error writing to save File: {e}");

            }
            finally
            {
                
                stream.Close();
                
            }

        }

        public void LoadGameData()
        {

            FileStream stream = new FileStream(savePath, FileMode.Open);
            
            try
            {

                //setupping formatter for loading data
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //loading user credentials
                users = (Dictionary<string, Player>) binaryFormatter.Deserialize(stream);

                Console.WriteLine(
                    $"\nSave file stuff:\nUsers size: {users.Count}\nUser #1: {users.FirstOrDefault().Key}");

            }
            catch (Exception e)
            {

                Console.WriteLine($"Error reading from save File: {e}");

            }
            finally
            {

                stream.Close();

            }
            
        }

        //creates fresh save data
        public void CreateSaveData()
        {
            
            users = new Dictionary<string, Player>();
            
            SaveGameData();

        }

        #endregion
        
        #region Access
        
        public Player NewPlayer(int id, string username)
        {
            
            //Check if player exists
            if (users.ContainsKey(username))
            {
                users[username].SetCurrentScene(1);
                return users[username].Connect(id);

            }
            else
            {
                
                //creates new player with supplied stuff, then adds it to players and saves it so its registered
                Player newPly =  new Player(id, username, 0);
                
                users.Add(username, newPly);
                SaveGameData();

                return newPly;

            }
            
        }

        #endregion

        #region Debug

        public void DisplayPlayersInfo()
        {

            Console.WriteLine("\nPLAYERS INFO\n\n");
            foreach (var c in Server.clients.Values)
            {

                if (c.player != null)
                {

                    Console.WriteLine($"#{c.id}: {c.player.username}, currently in scene #{c.player.currentScene} at pos [{c.player.posX}, {c.player.posY}, {c.player.posZ}]");

                }

            }
            Console.WriteLine("\n");

        }

        #endregion

    }
}