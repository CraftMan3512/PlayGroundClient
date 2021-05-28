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

        controlsScript = new BasicPlayerControls(head, GetComponent<Rigidbody>());
        InvokeRepeating(nameof(SendPlayerPosToServer),0,0.033333f);

    }

    // Update is called once per frame
    void Update()
    {

        controlsScript.MovePlayer(gameObject);

    }
    
    private void FixedUpdate()
    {

    }

    private void SendPlayerPosToServer()
    {

        if (Client.instance != null)
        {
            
            ClientSend.PlayerPosition(transform.position);   
            
        }

    }
}
