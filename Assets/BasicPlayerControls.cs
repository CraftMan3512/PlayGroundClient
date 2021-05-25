using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerControls : IPlayerControls
{

    public float speed = 1f;
    
    public void MovePlayer(GameObject player)
    {

        int down = Input.GetKey(KeyCode.S) ? 1 : 0;
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;
        player.transform.position += new Vector3(up - down, 0, right - left)*Time.deltaTime*speed;

    }
}