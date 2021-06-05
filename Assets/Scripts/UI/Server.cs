using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class Server
{
 
    public string ip { get; }
    public string username { get; }

    public Server(string _ip, string _username)
    {

        ip = _ip;
        username = _username;

    }
    
}