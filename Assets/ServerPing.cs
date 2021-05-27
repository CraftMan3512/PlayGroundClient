using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerPing : MonoBehaviour
{

    public TMP_InputField ip;

    public Image connectivityIndicator;

    public Button playButton;
    
    public void ConnectToServer()
    {
        
        Client.instance.ConnectToServer(ip.text);
        
    }

    public async void PingServer()
    {

        Debug.Log("PINGING SERVER!");
        if (await PingHost())
        {
            
            Debug.Log("PING SUCCESSFUL!");
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
            using (var client = new TcpClient(ip.text, Client.port))
            {
                await Task.Delay(10);
                return true;   
                
            }
        }
        catch (SocketException ex)
        {
            Debug.Log("Error pinging host:'" + ip.text + ":" + Client.port + "'");
            return false;
        }
    }
}
