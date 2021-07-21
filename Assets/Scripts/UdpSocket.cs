using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;

public class UdpSocket : MonoBehaviour
{

    public UdpClient client;
    public IPEndPoint ipEndPoint;
    public IPEndPoint endPoint;
    public Thread thread;

    public byte[] receivedData;
    public string dataString;

    public bool socketOpen = false;

    public void CreateSocket(int port, string ip)
    {   
        Debug.Log("Create new Udp client on port " + port.ToString());

        receivedData = new byte[0];

        //thread = new Thread(ReceiveData);
        //thread.Start();

        //ipEndPoint = new IPEndPoint(IPAddress.Any, port);
        //client = new UdpClient(ipEndPoint);
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
             
            //dataString = dataString.Substring(17,24);

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
