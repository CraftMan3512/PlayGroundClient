using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Text;

namespace PlayGroundServer
{

    [Serializable]
    public class Player //This class is the representation of the player cursor and where the player is scene-wise
    {

        [NonSerialized] //dont serialize the id since it may change every time a player disconnects
        public int id;
        
        public string username;

        public double posX,posY,posZ;
        [NonSerialized]
        public Vector3 playerRotation;

        public int currentScene { get; private set; }

        public Player(int _id, string _username, int startingScene)
        {

            id = _id;
            username = _username;
            posX = 0;
            posY = 0;
            posZ = 0;
            currentScene = startingScene;
            playerRotation = Vector3.Zero;
            currentScene = Constants.TITLESCREEN_SCENEINDEX;

        }
        
        public Player(int _id, string _username, Vector3 _playerPos)
        {

            id = _id;
            username = _username;
            posX = _playerPos.X;
            posY = _playerPos.Y;
            posZ = _playerPos.Z;
            playerRotation = Vector3.Zero;
            currentScene = Constants.TITLESCREEN_SCENEINDEX;

        }

       
        public void SetCurrentScene(int newScene)
        {

            currentScene = newScene;

        }

        public Player Connect(int _id)
        {

            id = _id;
            return this;

        }

        public void Update()
        {



        }
        
    }
}
