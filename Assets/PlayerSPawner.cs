using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSPawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        ClientSend.SpawnPlayers();
        
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
