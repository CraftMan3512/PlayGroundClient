using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Server
{
 
    public string ip { get; set; }
    public string username { get; set; }

    public Server(string _ip, string _username)
    {

        ip = _ip;
        username = _username;

    }
    
}