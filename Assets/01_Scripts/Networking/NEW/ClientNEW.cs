using UnityEngine;
using Unity.Collections;
using Unity.Networking.Transport;
using TMPro;

public class ClientNEW : MonoBehaviour
{
    public NetworkDriver m_Driver;
    public NetworkConnection m_Connection;
    public TextMeshProUGUI playerNameText;

    private string playerName;

    [SerializeField] private GameObject _JoinPanel;

    void Start()
    {
        m_Driver = NetworkDriver.Create();
        m_Connection = default(NetworkConnection);
        playerName = PlayerPrefs.GetString("Nickname", ""); // Retrieve player nickname from PlayerPrefs
    }

    void OnDestroy()
    {
        m_Driver.Dispose();
        // Clear the player's name from PlayerPrefs when the client is destroyed
        PlayerPrefs.DeleteKey("Nickname");
    }

    public void ConnectToServer()
    {
        var endpoint = NetworkEndpoint.LoopbackIpv4;
        endpoint.Port = 9000;
        m_Connection = m_Driver.Connect(endpoint);

        _JoinPanel.SetActive(false); 
    }

    public void SendMoveToServer(int index)
    {
        // Begin sending data
        m_Driver.BeginSend(m_Connection, out var writer);

        // Write the message type and data to the writer
        writer.WriteByte((byte)ClientMessageType.PlayerMove);
        writer.WriteInt(index);

        // End sending data
        m_Driver.EndSend(writer);
    }

    void UpdatePlayerInfo()
    {
        playerNameText.text = playerName; // Assuming playerName is set elsewhere
    }

    void Update()
    {
        m_Driver.ScheduleUpdate().Complete();

        if (!m_Connection.IsCreated)
            return;

        DataStreamReader stream;
        NetworkEvent.Type cmd;
        while ((cmd = m_Connection.PopEvent(m_Driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                Debug.Log("We are now connected to the server");
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                var messageType = (ServerMessageType)stream.ReadByte();
                switch (messageType)
                {
                    default:
                        Debug.Log("Unknown message type received from server");
                        break;
                }
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client got disconnected from server");
                m_Connection = default(NetworkConnection);
            }
        }
    }
}
