using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
   
    private ICameraControls cameraControl;
    private bool localplayer;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.parent.tag.Equals("LocalPlayer"))
        {
            localplayer = true;
        }
        cameraControl = new BasicCameraControls();
        ToggleCursor(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (localplayer)
        {
            cameraControl.MoveCamera(gameObject);
        }
    }

    private void FixedUpdate()
    {

        if (Client.instance != null && localplayer)
        {

            SendRotationToServer();

        }
        
    }

    private void SendRotationToServer()
    {

        ClientSend.PlayerRotation(transform.eulerAngles);

    }

    public void ToggleCursor(bool toggled)
    {
        Cursor.visible = toggled;
    }
}
