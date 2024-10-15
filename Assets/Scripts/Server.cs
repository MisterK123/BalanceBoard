using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System;
using TMPro;
using System.Text;
using System.Threading;


public class Server : MonoBehaviour
{

    private TcpListener _server;
    private Thread _serverThread;
    private bool _isRunning;

    // Start is called before the first frame update
    void Start()
    {
        StartServer();
    }

    private void StartServer()
    {
        _serverThread = new Thread(ServerThread);
        _serverThread.IsBackground = true;
        _serverThread.Start();
        _isRunning = true;
        Debug.Log("Server started...");
    }

    private void ServerThread()
    {
        _server = new TcpListener(IPAddress.Any, 54635); // Port 8888, you can change it to whatever you need
        _server.Start();

        try
        {
            while (_isRunning)
            {
                // Wait for a client connection
                Debug.Log("listening");
                TcpClient client = _server.AcceptTcpClient();
                Debug.Log("Client connected");

                // Start a thread to handle the client
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Server error: {e.Message}");
        }
        finally
        {
            _server.Stop();
        }
    }

    private void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (client.Connected)
        {
            try
            {
                // Read data from the client
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break; // Client disconnected

                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.Log($"Received: {data}");

                // You can send data back to the client if needed
                // byte[] responseData = Encoding.UTF8.GetBytes("Response from server");
                // stream.Write(responseData, 0, responseData.Length);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error handling client: {e.Message}");
                break;
            }
        }

        Debug.Log("Client disconnected");
        client.Close();
    }

    private void OnApplicationQuit()
    {
        _isRunning = false;
        _server.Stop();
        _serverThread?.Join();
        Debug.Log("Server stopped");
    }
}