using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class NpcHistoryPanel : MonoBehaviour
{
    public GameObject buttonPrefab; // Assign a prefab with a Button + TMP_Text
    public Transform buttonParent;  // UI container for the buttons
    public NpcLoader npcLoader;     // Reference to your script that fills the generator fields

    private string npcFolder;

    void Start()
    {
        npcFolder = Path.Combine(Application.persistentDataPath, "NPCs");
        LoadRecentNpcButtons();
    }

    public void LoadRecentNpcButtons()
    {
        // Clear old buttons
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        if (!Directory.Exists(npcFolder)) return;

        var files = Directory.GetFiles(npcFolder, "npc_*.json")
                             .OrderByDescending(File.GetLastWriteTime)
                             .Take(5);

        foreach (var file in files)
        {
            string json = File.ReadAllText(file);
            var npc = JsonUtility.FromJson<NpcApiClient.NpcData>(json);

            string nameToShow = ExtractNameFromJson(json) ?? "(No Name)";
            string time = File.GetLastWriteTime(file).ToString("yyyy-MM-dd HH:mm");

            CreateButton(nameToShow, npc, time);
        }

        // ✅ Force layout update after buttons are added
        LayoutRebuilder.ForceRebuildLayoutImmediate(buttonParent.GetComponent<RectTransform>());
    }


    void CreateButton(string name, NpcApiClient.NpcData npc, string timestamp)
    {
        var buttonGO = Instantiate(buttonPrefab, buttonParent);
        var buttonText = buttonGO.GetComponentInChildren<TMP_Text>();

        // Add rich text with smaller font size for time
        buttonText.text = $"Name: {name}\n<size=70%>{timestamp}</size>";

        buttonGO.GetComponent<Button>().onClick.AddListener(() =>
        {
            npcLoader.LoadNpc(npc);
            npcLoader.ShowGeneratorPanel();
        });
    }

    string ExtractNameFromJson(string json)
    {
        int startIndex = json.IndexOf("\"name\":");
        if (startIndex >= 0)
        {
            int quoteStart = json.IndexOf('"', startIndex + 7);
            int quoteEnd = json.IndexOf('"', quoteStart + 1);
            return json.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
        }
        return null;
    }

    public void ClearCurrentHistory()
    {
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void DeleteAllSavedNpcs()
    {
        if (!Directory.Exists(npcFolder)) return;

        var files = Directory.GetFiles(npcFolder, "npc_*.json");
        foreach (var file in files)
        {
            File.Delete(file);
        }

        // Refresh the UI after deletion
        ClearCurrentHistory();
    }
}