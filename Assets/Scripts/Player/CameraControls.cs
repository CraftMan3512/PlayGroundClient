using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
   
    private ICameraControls cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        cameraControl = new BasicCameraControls();
        ToggleCursor(false);
    }

    // Update is called once per frame
    void Update()
    {
        cameraControl.MoveCamera(gameObject);
    }

    public void ToggleCursor(bool toggled)
    {
        Cursor.visible = toggled;
    }
}
