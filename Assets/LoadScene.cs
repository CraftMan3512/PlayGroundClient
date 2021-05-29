using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{

    public SceneTypes scene;
    public void ChangeScene(GameObject player)
    {

        SceneChanger.ChangeScene(scene);
        
    }
    
    
}
