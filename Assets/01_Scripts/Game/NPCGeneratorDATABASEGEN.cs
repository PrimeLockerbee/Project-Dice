using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NPCGeneratorDATABASEGEN : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField nameField;
    public TMP_InputField descriptionField;
    public TMP_InputField plotHookField;
    public TMP_InputField statsField;
    public TMP_InputField occupationField;
    public TMP_InputField appearanceField;
    public TMP_InputField personalityField;
    public TMP_InputField inventoryField;
    public TMP_InputField quoteField;
    public TMP_InputField backStoryField;

    [Header("Setup Reference")]
    public GeneratorSetup setup; // Assign your GeneratorSetup script here in Inspector

    private string baseUrl = "https://lockerbeeprime.x10.mx/get_npc.php";

    private string[] statNames = {
        "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma"
    };

    public void GenerateNPC()
    {
        List<string> fields = new List<string>();

        if (setup.nameToggle.isOn) fields.Add("name");
        if (setup.descriptionToggle.isOn) fields.Add("description");
        if (setup.plotHookToggle.isOn) fields.Add("plot_hook");
        if (setup.occupationToggle.isOn) fields.Add("occupation");
        if (setup.appearanceToggle.isOn) fields.Add("appearance");
        if (setup.personalityToggle.isOn) fields.Add("personality");
        if (setup.inventoryToggle.isOn) fields.Add("inventory");
        if (setup.quoteToggle.isOn) fields.Add("quote");
        if (setup.backstoryToggle.isOn) fields.Add("backstory");
        // Stats handled locally, so do not add here

        string url = baseUrl;
        if (fields.Count > 0)
        {
            url += "?fields=" + string.Join(",", fields);
        }

        StartCoroutine(GetNPCData(url));
    }

    IEnumerator GetNPCData(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;

                Debug.Log("Received JSON: " + json); // ← Toevoegen voor debug

                // Parse JSON as dictionary for flexibility
                SerializableDictionary npcData = JsonUtility.FromJson<SerializableDictionary>(json);

                if (setup.nameToggle.isOn && npcData.ContainsKey("name"))
                    nameField.text = npcData["name"];
                if (setup.descriptionToggle.isOn && npcData.ContainsKey("description"))
                    descriptionField.text = npcData["description"];
                if (setup.plotHookToggle.isOn && npcData.ContainsKey("plot_hook"))
                    plotHookField.text = npcData["plot_hook"];
                if (setup.occupationToggle.isOn && npcData.ContainsKey("occupation"))
                    occupationField.text = npcData["occupation"];
                if (setup.appearanceToggle.isOn && npcData.ContainsKey("appearance"))
                    appearanceField.text = npcData["appearance"];
                if (setup.personalityToggle.isOn && npcData.ContainsKey("personality"))
                    personalityField.text = npcData["personality"];
                if (setup.inventoryToggle.isOn && npcData.ContainsKey("inventory"))
                    inventoryField.text = npcData["inventory"];
                if (setup.quoteToggle.isOn && npcData.ContainsKey("quote"))
                    quoteField.text = npcData["quote"];
                if (setup.backstoryToggle.isOn && npcData.ContainsKey("backstory"))
                    backStoryField.text = npcData["backstory"];

                // Generate stats locally if toggled
                if (setup.statsToggle.isOn)
                {
                    statsField.text = GenerateRandomStats();
                }
            }
        }
    }

    private string GenerateRandomStats()
    {
        string leftStats = "";
        string rightStats = "";

        for (int i = 0; i < statNames.Length; i++)
        {
            int randomValue = Random.Range(8, 21); // 8 to 20 inclusive
            string statLine = $"{statNames[i]}: {randomValue}";

            if (i < 3) // First 3 stats on left
                leftStats += statLine.PadRight(20);
            else // Last 3 stats on right
                rightStats += statLine.PadRight(20);
        }

        return $"{leftStats}\n{rightStats}";
    }

    // Helper class for flexible JSON parsing into Dictionary<string, string>
    [System.Serializable]
    public class SerializableDictionary : ISerializationCallbackReceiver
    {
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        private Dictionary<string, string> _dict = new Dictionary<string, string>();

        public string this[string key]
        {
            get => _dict.ContainsKey(key) ? _dict[key] : "";
            set => _dict[key] = value;
        }

        public bool ContainsKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (var kvp in _dict)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            _dict = new Dictionary<string, string>();
            for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
            {
                _dict[keys[i]] = values[i];
            }
        }
    }
}
