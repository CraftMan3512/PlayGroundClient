using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private IPlayerControls controlsScript;
    public GameObject head;

    public static bool exists = false;
    // Start is called before the first frame update
    void Awake()
    {

        if (exists) Destroy(gameObject);
        else exists = true;
        controlsScript = new BasicPlayerControls(head, GetComponent<Rigidbody>());
      

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
            
            SendPlayerPosToServer();

        }

    }

    private void SendPlayerPosToServer()
    {
        
        ClientSend.PlayerPosition(transform.position);
        
    }
}
