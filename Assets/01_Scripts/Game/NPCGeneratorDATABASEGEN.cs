using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NPCGeneratorDATABASEGEN : MonoBehaviour
{
    public MarkovNameGenerator markovNameGenerator; // Assign in inspector

    public Button generateButton; // Assign in Inspector

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
    public GeneratorSetup setup;

    private string baseUrl = "https://studenthome.hku.nl/~bradley.vanewijk/get_npc.php";

    private string[] statNames = {
        "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma"
    };

    public void GenerateNPC()
    {
        generateButton.interactable = false; // disable button
        StartCoroutine(ReenableButtonAfterDelay(3f)); // re-enable after 3 seconds

        List<string> fields = new List<string>();

        if (setup.descriptionToggle.isOn) fields.Add("description");
        if (setup.plotHookToggle.isOn) fields.Add("plot_hook");
        if (setup.occupationToggle.isOn) fields.Add("occupation");
        if (setup.appearanceToggle.isOn) fields.Add("appearance");
        if (setup.personalityToggle.isOn) fields.Add("personality");
        if (setup.inventoryToggle.isOn) fields.Add("inventory");
        if (setup.quoteToggle.isOn) fields.Add("quote");
        if (setup.backstoryToggle.isOn) fields.Add("backstory");

        string url = baseUrl;
        if (fields.Count > 0)
        {
            url += "?fields=" + string.Join(",", fields);
        }

        StartCoroutine(GetNPCData(url));
    }

    IEnumerator GetNPCData(string url)
    {
        int attempts = 0;
        const int maxAttempts = 3;

        while (attempts < maxAttempts)
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

                if (string.IsNullOrWhiteSpace(json))
                {
                    Debug.LogError("Empty or invalid JSON: " + json);
                    attempts++;
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                if (json.Contains("\"error\""))
                {
                    Debug.LogError("Server returned error: " + json);
                    yield break;
                }

                NPCData npcData = null;
                bool parseFailed = false;
                try
                {
                    npcData = JsonUtility.FromJson<NPCData>(json);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to parse JSON: " + e.Message);
                    parseFailed = true;
                }

                if (parseFailed || npcData == null)
                {
                    Debug.LogError("Parsed NPC data is null or parsing failed.");
                    attempts++;
                    yield return new WaitForSeconds(1f);
                    continue;
                }

                // Assign the fields based on toggles
                if (setup.nameToggle.isOn)
                {
                    string generatedName = markovNameGenerator.GenerateName();
                    nameField.text = generatedName;
                }

                if (setup.descriptionToggle.isOn)
                    descriptionField.text = npcData.description ?? "";
                if (setup.plotHookToggle.isOn)
                    plotHookField.text = npcData.plot_hook ?? "";
                if (setup.occupationToggle.isOn)
                    occupationField.text = npcData.occupation ?? "";
                if (setup.appearanceToggle.isOn)
                    appearanceField.text = npcData.appearance ?? "";
                if (setup.personalityToggle.isOn)
                    personalityField.text = npcData.personality ?? "";
                if (setup.inventoryToggle.isOn)
                    inventoryField.text = npcData.inventory ?? "";
                if (setup.quoteToggle.isOn)
                    quoteField.text = npcData.quote ?? "";
                if (setup.backstoryToggle.isOn)
                    backStoryField.text = npcData.backstory ?? "";

                if (setup.statsToggle.isOn)
                {
                    statsField.text = GenerateRandomStats();
                }

                yield break; // success, exit coroutine
            }
        }

        Debug.LogError("Failed to get valid NPC data after " + maxAttempts + " attempts.");
    }


    private string GenerateRandomStats()
    {
        string leftStats = "";
        string rightStats = "";

        for (int i = 0; i < statNames.Length; i++)
        {
            int randomValue = Random.Range(8, 21);
            string statLine = $"{statNames[i]}: {randomValue}";

            if (i < 3)
                leftStats += statLine.PadRight(20);
            else
                rightStats += statLine.PadRight(20);
        }

        return $"{leftStats}\n{rightStats}";
    }

    public void CopyAllNPCFieldsToClipboard()
    {
        string npcText = "";

        if (setup.nameToggle.isOn)
            npcText += $"Name: {nameField.text}\n";
        if (setup.descriptionToggle.isOn)
            npcText += $"Description: {descriptionField.text}\n";
        if (setup.plotHookToggle.isOn)
            npcText += $"Plot Hook: {plotHookField.text}\n";
        if (setup.occupationToggle.isOn)
            npcText += $"Occupation: {occupationField.text}\n";
        if (setup.appearanceToggle.isOn)
            npcText += $"Appearance: {appearanceField.text}\n";
        if (setup.personalityToggle.isOn)
            npcText += $"Personality: {personalityField.text}\n";
        if (setup.inventoryToggle.isOn)
            npcText += $"Inventory: {inventoryField.text}\n";
        if (setup.quoteToggle.isOn)
            npcText += $"Quote: \"{quoteField.text}\"\n";
        if (setup.backstoryToggle.isOn)
            npcText += $"Backstory: {backStoryField.text}\n";
        if (setup.statsToggle.isOn)
            npcText += $"Stats:\n{statsField.text}\n";

        GUIUtility.systemCopyBuffer = npcText;
        Debug.Log("NPC copied to clipboard:\n" + npcText);
    }

    private IEnumerator ReenableButtonAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        generateButton.interactable = true;
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
