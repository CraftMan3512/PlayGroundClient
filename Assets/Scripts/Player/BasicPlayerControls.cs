using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerControls : IPlayerControls
{
    GameObject head;
    public float speed = 3f;
    
   public BasicPlayerControls(GameObject _head)
    {
        head = _head;
    }

    public void MovePlayer(GameObject player)
    {

        int down = Input.GetKey(KeyCode.S) ? 1 : 0;
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;
        player.transform.position +=  Quaternion.AngleAxis(head.transform.eulerAngles.y,Vector3.up)*(new Vector3((right - left), 0, (up - down)) * Time.deltaTime * speed);
      
    }
}