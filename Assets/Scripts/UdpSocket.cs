using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpSocket : MonoBehaviour
{

    public UdpClient client;
    public IPEndPoint endPoint;

    public byte[] receivedData;
    public string dataString;

    public bool socketOpen = false;

    public void CreateSocket(int port)
    {   
        Debug.Log("Create new Udp client on port " + port.ToString());

        receivedData = new byte[0];
        client = new UdpClient(port);
        endPoint = new IPEndPoint(IPAddress.Any, 0);

        socketOpen = true;
    }

    public string ReceiveData()
    {
        if (client.Available > 0)
         {
            receivedData = client.Receive(ref endPoint);
            dataString = Encoding.ASCII.GetString(receivedData);

            return dataString;
         }
         else
         {
             return "";
         }
    }

    public void CloseSocket()
    {   
        if(socketOpen) 
        {
            socketOpen = false;
            client.Close();
        } 
    }
}
