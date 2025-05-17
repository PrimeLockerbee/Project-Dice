using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NPCGeneratorDATABASEGEN : MonoBehaviour
{
    public MarkovNameGenerator markovNameGenerator; // Assign in inspector

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

        //if (setup.nameToggle.isOn) fields.Add("name");
        if (setup.descriptionToggle.isOn) fields.Add("description");
        if (setup.plotHookToggle.isOn) fields.Add("plot_hook");
        if (setup.occupationToggle.isOn) fields.Add("occupation");
        if (setup.appearanceToggle.isOn) fields.Add("appearance");
        if (setup.personalityToggle.isOn) fields.Add("personality");
        if (setup.inventoryToggle.isOn) fields.Add("inventory");
        if (setup.quoteToggle.isOn) fields.Add("quote");
        if (setup.backstoryToggle.isOn) fields.Add("backstory");
        // Stats handled locally

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
                yield break;
            }

            string json = www.downloadHandler.text;
            Debug.Log("Received JSON: '" + json + "'");

            // Skip if empty or PHP error returned as HTML
            if (string.IsNullOrEmpty(json) || json.StartsWith("<"))
            {
                Debug.LogError("Empty or invalid JSON: " + json);
                yield break;
            }

            NPCData npcData = JsonUtility.FromJson<NPCData>(json);

            if (setup.nameToggle.isOn)
            {
                // Use Markov generator to get name
                string generatedName = markovNameGenerator.GenerateName();
                nameField.text = generatedName;
            }

            if (setup.descriptionToggle.isOn && npcData != null)
                descriptionField.text = npcData.description ?? "";
            if (setup.plotHookToggle.isOn && npcData != null)
                plotHookField.text = npcData.plot_hook ?? "";
            if (setup.occupationToggle.isOn && npcData != null)
                occupationField.text = npcData.occupation ?? "";
            if (setup.appearanceToggle.isOn && npcData != null)
                appearanceField.text = npcData.appearance ?? "";
            if (setup.personalityToggle.isOn && npcData != null)
                personalityField.text = npcData.personality ?? "";
            if (setup.inventoryToggle.isOn && npcData != null)
                inventoryField.text = npcData.inventory ?? "";
            if (setup.quoteToggle.isOn && npcData != null)
                quoteField.text = npcData.quote ?? "";
            if (setup.backstoryToggle.isOn && npcData != null)
                backStoryField.text = npcData.backstory ?? "";

            if (setup.statsToggle.isOn)
            {
                statsField.text = GenerateRandomStats();
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

            if (i < 3)
                leftStats += statLine.PadRight(20);
            else
                rightStats += statLine.PadRight(20);
        }

        return $"{leftStats}\n{rightStats}";
    }

    [System.Serializable]
    public class NPCData
    {
        public string name;
        public string description;
        public string plot_hook;
        public string occupation;
        public string appearance;
        public string personality;
        public string inventory;
        public string quote;
        public string backstory;
    }
}
