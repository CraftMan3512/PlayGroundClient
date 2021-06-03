using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{

    public Camera camera;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.6f, this.transform.position.z);
    }
}
