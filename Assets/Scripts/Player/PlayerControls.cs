using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private IPlayerControls controlsScript;
    public GameObject head;
    // Start is called before the first frame update
    void Start()
    {

        controlsScript = new BasicPlayerControls(head);
        

    }

    // Update is called once per frame
    void Update()
    {

        controlsScript.MovePlayer(gameObject);

    }
    
    private void FixedUpdate()
    {

        if (Client.instance != null)
        {
            
            SendPlayerPosToServer();//TODO PROGRAMMER CETTE FONCTION LA

        }

    }

    private void SendPlayerPosToServer()
    {
        
        ClientSend.PlayerPosition(transform.position);
        
    }
}
