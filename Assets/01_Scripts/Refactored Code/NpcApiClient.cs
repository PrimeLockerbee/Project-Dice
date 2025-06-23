using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NpcApiClient : MonoBehaviour
{
    public string apiUrl = "https://studenthome.hku.nl/~bradley.vanewijk/get_npc.php";

    // Define a public event for when NPC data is received
    public event Action<NpcData> OnNpcDataReceived;

    // Define a simple data container class to hold NPC fields
    [Serializable]
    public class NpcData
    {
        public string name;
        public string race;
        public string alignment;
        public string stats;
        public string description;
        public string plot_hook;
        public string occupation;
        public string appearance;
        public string personality;
        public string inventory;
        public string quote;
        public string backstory;
    }

    // Call this method to request NPC data from your API
    public void RequestNpcData(string fields = "description,plot_hook,occupation")
    {
        StartCoroutine(FetchNpcData(fields));
    }

    private IEnumerator FetchNpcData(string fields)
    {
        string url = $"{apiUrl}?fields={fields}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"API request error: {request.error}");
                yield break;
            }

            string json = request.downloadHandler.text;
            NpcData npcData = JsonUtility.FromJson<NpcData>(json);

            if (npcData != null)
            {
                OnNpcDataReceived?.Invoke(npcData);
            }
            else
            {
                Debug.LogWarning("Received empty or invalid NPC data");
            }
        }
    }
}