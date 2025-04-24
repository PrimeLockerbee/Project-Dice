using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

public class Client : MonoBehaviour
{
    public string serverAddress = "127.0.0.1";
    public int serverPort = 8888;

    private TcpClient client;
    private NetworkStream networkStream;

    private byte[] receiveBuffer = new byte[4096];
    private string receivedData = "";

    public Server server; // Store the Server reference

    private void Start()
    {
        //if (gameManager != null)
        //{
        //    Debug.Log("GameManager reference assigned.");
        //}
        //else
        //{
        //    Debug.LogWarning("GameManager reference not found.");
        //}
    }

    public void ConnectToServer()
    {
        try
        {
            client = new TcpClient();
            client.BeginConnect(serverAddress, serverPort, ConnectCallback, null);
        }
        catch (Exception e)
        {
            Debug.Log("Error connecting to server: " + e.Message);
        }
    }

    public void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            client.EndConnect(ar);
            if (client.Connected)
            {
                Debug.Log("Connected to server.");

                networkStream = client.GetStream();
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveCallback, null);
            }
            else
            {
                Debug.Log("Failed to connect to server.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error during connect callback: " + e.Message);
        }
    }

    public void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int bytesRead = networkStream.EndRead(ar);
            if (bytesRead <= 0)
            {
                Debug.Log("Disconnected from server.");
                client.Close();
                return;
            }

            receivedData += System.Text.Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);

            // Split receivedData into individual messages using the newline delimiter
            string[] messages = receivedData.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Process each message
            foreach (string message in messages)
            {
                ProcessData(message);
            }

            // Clear receivedData after processing
            receivedData = "";

            // Continue reading from the stream
            networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Debug.Log("Error during receive callback: " + e.Message);
        }
    }


    public void ProcessData(string data)
    {
        string[] messages = data.Split(new string[] { "###" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string message in messages)
        {
            Debug.Log("Received message: " + message);

            if (message.StartsWith("PLAYER_NUMBER:"))
            {
                int playerNumber = int.Parse(message.Substring(14));
                Debug.Log("Assigned player number: " + playerNumber);
            }
            else if (message.StartsWith("START_GAME:"))
            {
                int firstPlayer = int.Parse(message.Substring(11));
                Debug.Log("Starting game with first player: " + firstPlayer);
            }
            else if (message.StartsWith("SWITCH_PLAYER")) // Check if the playerNumber matches
            {
                Debug.Log("Switch Player");
            }
            else if (message.StartsWith("MOVE:"))
            {
                // Extract the move index from the message
                int moveIndex = int.Parse(message.Substring(5));

                Debug.Log("Move index: " + moveIndex);
            }
            else if (message == "WIN")
            {
                Debug.LogWarning("Received WIN message, but no win condition found.");
            }
            else if (message == "DRAW")
            {
                Debug.LogWarning("Received DRAW message, but the game is not a draw.");
            }
        }
    }

    public void SendMove(int cellIndex)
    {
        string moveData = "MOVE:" + cellIndex;
        SendDataToServer(moveData);
    }

    public void SendDataToServer(string message)
    {
        byte[] messageData = Encoding.ASCII.GetBytes(message);
        networkStream.Write(messageData, 0, messageData.Length);
        networkStream.Flush(); // Add this line to flush the data immediately
        Debug.Log("Sent message to server: " + message);
    }

    public void SendData(string data)
    {
        try
        {
            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(data + "\n"); // Append newline delimiter
            networkStream.Write(dataBuffer, 0, dataBuffer.Length);
        }
        catch (Exception e)
        {
            Debug.Log("Error sending data: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        if (client != null && client.Connected)
        {
            client.Close();
        }
    }
}