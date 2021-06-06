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

    private static string serversSaveName = "/PlaygroundServers.json";
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
        
        string path = Application.persistentDataPath + serversSaveName;

        string json = JsonConvert.SerializeObject(servers);
        using (var stream = File.Create(path))
        {

            var writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Close();
            stream.Close();
            
        }


    }
    
    public static void LoadServers()
    {
        
        string path = Application.persistentDataPath + serversSaveName;

        if(File.Exists(path))
        {

            string text = File.ReadAllText(path);
            servers = JsonConvert.DeserializeObject<List<Server>>(text);

        } else
        {
            
            //pas de save, on crée une liste vide
            servers = new List<Server>();

        }
        
    }
    
    
}
