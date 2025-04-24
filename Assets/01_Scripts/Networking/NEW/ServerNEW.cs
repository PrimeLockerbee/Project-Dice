using UnityEngine;
using Unity.Collections;
using Unity.Networking.Transport;
using System;

public class ServerNEW : MonoBehaviour
{
    public NetworkDriver _Driver;
    public NetworkConnection[] _Connections;

    private bool _IsServerRunning = false; // Flag to track if the server is already running

    [SerializeField] private ClientNEW _ClientNew;

    void Start()
    {
        _Connections = new NetworkConnection[2];
    }

    void OnDestroy()
    {
        if (_Driver.IsCreated)
        {
            _Driver.Dispose();
            Debug.Log("Server stopped");
        }
    }

    void Update()
    {
        if (!_IsServerRunning)
            return;

        _Driver.ScheduleUpdate().Complete();

        // Accept new connections
        AcceptConnection();

        // Process events for existing connections
        DataStreamReader stream;
        for (int i = 0; i < _Connections.Length; i++)
        {
            if (!_Connections[i].IsCreated)
                continue;

            NetworkEvent.Type cmd;
            while ((cmd = _Driver.PopEventForConnection(_Connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    // Process data received from the connection
                    ProcessClientMessage(stream, i);
                }
            }
        }
    }

    void AcceptConnection()
    {
        NetworkConnection c;
        while ((c = _Driver.Accept()) != default(NetworkConnection))
        {
            for (int i = 0; i < _Connections.Length; i++)
            {
                if (!_Connections[i].IsCreated)
                {
                    _Connections[i] = c;
                    Debug.Log("Accepted a connection");

                    Debug.Log("Total connections: " + _Connections.Length);
                    break;
                }
                else if (i == _Connections.Length - 1)
                {
                    Debug.Log("Rejected a connection - Server full");
                    c.Disconnect(_Driver);
                }
            }
        }
    }


    void ProcessClientMessage(DataStreamReader stream, int connectionIndex)
    {
        var messageType = (ClientMessageType)stream.ReadByte();

        Debug.Log($"{messageType}");    

        switch (messageType)
        {
            default:
                Debug.Log("Unknown message type received from client");
                break;
        }
    }


    void SendServerMessage(NetworkConnection connection, ServerMessageType messageType, int data)
    {
        // Begin sending data
        _Driver.BeginSend(connection, out var writer);

        // Write the message type and data to the writer
        writer.WriteByte((byte)messageType);
        writer.WriteInt(data);

        // End sending data
        _Driver.EndSend(writer);
    }

    public void StartServer()
    {
        if (!_IsServerRunning)
        {
            _Driver = NetworkDriver.Create();
            var endpoint = NetworkEndpoint.AnyIpv4;
            endpoint.Port = 9000;
            if (_Driver.Bind(endpoint) != 0)
            {
                Debug.Log("Failed to bind to port 9000");
                return; // Exit the method if binding fails
            }

            _Driver.Listen();
            Debug.Log("Server started successfully");
            _IsServerRunning = true;

            _ClientNew.ConnectToServer();
        }
    }
}
