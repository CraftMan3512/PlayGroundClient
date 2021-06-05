using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

public class ServersManager : MonoBehaviour
{

    public static List<Server> servers;
    public GameObject viewServer;

    private void Start()
    {
        LoadServers();
    }

    public void DisplayServers()
    {
        
        if (servers == null) LoadServers();

        CreateViewServers();

    }

    private void CreateViewServers()
    {

        //clear view
        for (int i = 0; i < transform.childCount; i++)
        {
            
            Destroy(transform.GetChild(i).gameObject);
            
        }
        
        float height = viewServer.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < servers.Count; i++)
        {

            var s = servers[i];

            GameObject view = Instantiate(viewServer, Vector3.zero, Quaternion.identity, transform);
            view.GetComponent<RectTransform>().localPosition = new Vector3(0,-(i*height),0);
            view.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-(i*height),0);
            
            view.GetComponent<ViewServer>().SetServer(s);

        }
        
    }

    public static void SaveServers()
    {
        
        string path = Application.persistentDataPath + "/PlaygroundServers.txt";

        string json = JsonConvert.SerializeObject(servers);
        using (var stream = new FileStream(path, FileMode.Create))
        {

            var writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Close();
            stream.Close();
            
        }


    }
    
    public static void LoadServers()
    {
        
        string path = Application.persistentDataPath + "/PlaygroundServers.txt";

        if(File.Exists(path))
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {

                var reader = new StreamReader(stream);
                string text = reader.ReadLine();
                servers = (List<Server>) JsonConvert.DeserializeObject(text);
                reader.Close();
                stream.Close();
                
            }

        } else
        {
            
            //pas de save, on crée une liste vide
            servers = new List<Server>();

        }
        
    }
    
    
}
