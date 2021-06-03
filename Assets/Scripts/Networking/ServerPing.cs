using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ping = System.Net.NetworkInformation.Ping;

public class ServerPing : MonoBehaviour
{
    private static ServerPing instance;
    
    public TMP_InputField ip;

    public Image connectivityIndicator;

    public Button playButton;

    private void Awake()
    {

        instance = this;

    }

    public void ConnectToServer()
    {
        
        Client.instance.ConnectToServer(ip.text);
        
    }

    public async void PingServer()
    {
        
        if (await PingHost())
        {
            
            ErrorDisplayer.Log("PING SUCCESSFUL!");
            connectivityIndicator.color = Color.green;
            playButton.interactable = true;

        }
        else
        {
            
            connectivityIndicator.color = Color.red;
            playButton.interactable = false;

        }
        
    }
    
    private async Task<bool> PingHost()
    {
        try
        {

            //parse localhost
            string sendIp = string.Equals(ip.text, "localhost", StringComparison.CurrentCultureIgnoreCase) ? "127.0.0.1" : ip.text;

            //ping machine first, if it doesnt work, don't ping the port to save the freeze
 /*           using (Ping pingSender = new Ping())
            {

                var options = new PingOptions();

                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted.
                byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

                //send ping async
                PingReply reply = pingSender.Send(IPAddress.Parse(sendIp), 2000, buffer, options);

                if (reply.Status == IPStatus.Success)
                {*/

                    //success, ping port now
                    using (var client = new TcpClient())
                    {
                        client.SendTimeout = 2000;
                        client.ReceiveTimeout = 2000;
                        await client.ConnectAsync(IPAddress.Parse(sendIp), Client.port);
                        return true;

                    }

               /* }
                else
                {

                    throw new Exception("Could not resolve IP address, there is no server at this IP.");

                }

            }*/

        }
        catch (FormatException ex)
        {
            
            ErrorDisplayer.Log("U didnt enter an ip address >:(");
            return false;

        }
        catch (Exception ex)
        {
            ErrorDisplayer.Log($"Error pinging host:'" + ip.text + ":" + Client.port + $"', ex: {ex}");
            return false;
        }
    }
}
