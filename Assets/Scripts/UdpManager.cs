using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections;

public class UdpManager : MonoBehaviour
{
    public static UdpManager Instance { get; private set; }

    private UdpClient client;
    private IPEndPoint endPoint;
    public bool socketOpen = false;

    private float lastReceivedTime;
    public float connectionTimeout = 0.5f; 
    // public bool IsConnected => Time.time - lastReceivedTime < connectionTimeout;
    private bool isConnected = false;
    private bool isCheckingConnection = false;

    public bool IsConnected => isCheckingConnection ? isConnected : (Time.time - lastReceivedTime < connectionTimeout);
    public bool IsCheckingConnection => isCheckingConnection;

    public byte[] ReceivedData { get; private set; }
    public string DataString { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("UdpManager initialized!");
    }

    public void CreateSocket()
    {
        if (socketOpen)
        {
            Debug.LogWarning("UDP socket is already open.");
            return;
        }

        int port = PlayerPrefs.GetInt("port", 5555);

        try
        {
            client = new UdpClient(port);
            endPoint = new IPEndPoint(IPAddress.Any, 0);
            socketOpen = true;
            Debug.Log("UDP Socket listening on port " + port);
         }
        catch (Exception e)
        {
            Debug.LogError($"Failed to open UDP socket: {e.Message}");
            socketOpen = false;
        }
    }

    public string ReceiveData()
    {
        if (socketOpen && client.Available > 0)
        {
            ReceivedData = client.Receive(ref endPoint);
            DataString = Encoding.ASCII.GetString(ReceivedData);
            lastReceivedTime = Time.time;
            return DataString;
        }

        return string.Empty;
    }

    public void CloseSocket()
    {
        if (socketOpen)
        {
            client.Close();
            socketOpen = false;
        }
    }

    public void StartConnectionCheck(int durationInSeconds)
    {
        StartCoroutine(CheckConnectionDuringCountdown(durationInSeconds));
    }

    private IEnumerator CheckConnectionDuringCountdown(int duration)
{
    float endTime = Time.time + duration;
    isCheckingConnection = true;
    isConnected = false;

    float lastSignalTime = Time.time;

    while (Time.time < endTime && socketOpen)
    {
        if (client.Available > 0)
        {
            try
            {
                ReceivedData = client.Receive(ref endPoint);
                DataString = Encoding.ASCII.GetString(ReceivedData);
                lastSignalTime = Time.time;
                isConnected = true;

                Debug.Log("Received data during countdown.");
            }
            catch (Exception e)
            {
                Debug.LogError("Error receiving data: " + e.Message);
            }
        }

        // Check for 1-second signal timeout
        if (Time.time - lastSignalTime > 0.5f)
        {
            isConnected = false;
        }

        yield return null; // wait 1 frame
    }

    isCheckingConnection = false;

    if (!isConnected)
        Debug.Log("Connection check finished. No signal in last second.");
}

}
