using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using SFB;

public class NpcSaver : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInput;
    public TMP_InputField descriptionInput;
    public TMP_InputField plotHookInput;
    public TMP_InputField occupationInput;
    public TMP_InputField raceInput;
    public TMP_InputField alignmentInput;
    public TMP_InputField statsInput;
    public TMP_InputField appearanceInput;
    public TMP_InputField personalityInput;
    public TMP_InputField inventoryInput;
    public TMP_InputField quoteInput;
    public TMP_InputField backstoryInput;

    private string saveFolder;

    private void Awake()
    {
        saveFolder = Path.Combine(Application.persistentDataPath, "NPCs");
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }

    // Local NPC data class (independent of NpcApiClient)
    [System.Serializable]
    public class NpcData
    {
        public string name;
        public string description;
        public string plot_hook;
        public string occupation;
        public string race;
        public string alignment;
        public string stats;
        public string appearance;
        public string personality;
        public string inventory;
        public string quote;
        public string backstory;
    }

    // Save as JSON to default folder (same as before)
    public void SaveAsJson()
    {
        var npc = CollectNpcData();

        string json = JsonUtility.ToJson(npc, true);
        string fileName = $"npc_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        string fullPath = Path.Combine(saveFolder, fileName);

        File.WriteAllText(fullPath, json);
        Debug.Log($"NPC saved to: {fullPath}");
    }

    // Save as TXT with field names, with save dialog to choose folder and name
    public void SaveAsTxt()
    {
        var npc = CollectNpcData();

        var sb = new StringBuilder();
        AppendField(sb, "Name", npc.name);
        AppendField(sb, "Description", npc.description);
        AppendField(sb, "Plot Hook", npc.plot_hook);
        AppendField(sb, "Occupation", npc.occupation);
        AppendField(sb, "Race", npc.race);
        AppendField(sb, "Alignment", npc.alignment);
        AppendField(sb, "Stats", npc.stats);
        AppendField(sb, "Appearance", npc.appearance);
        AppendField(sb, "Personality", npc.personality);
        AppendField(sb, "Inventory", npc.inventory);
        AppendField(sb, "Quote", npc.quote);
        AppendField(sb, "Backstory", npc.backstory);

        var extensions = new[] {
            new ExtensionFilter("Text Files", "txt"),
            new ExtensionFilter("All Files", "*")
        };

        string path = StandaloneFileBrowser.SaveFilePanel("Save NPC as TXT", saveFolder, $"npc_{System.DateTime.Now:yyyyMMdd_HHmmss}.txt", extensions);

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, sb.ToString());
            Debug.Log($"NPC saved as TXT to: {path}");
        }
        else
        {
            Debug.Log("Save cancelled or failed.");
        }
    }

    private void AppendField(StringBuilder sb, string fieldName, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            sb.AppendLine($"{fieldName}: {value}");
        }
    }

    private NpcData CollectNpcData()
    {
        return new NpcData
        {
            name = nameInput?.text,
            description = descriptionInput?.text,
            plot_hook = plotHookInput?.text,
            occupation = occupationInput?.text,
            race = raceInput?.text,
            alignment = alignmentInput?.text,
            stats = statsInput?.text,
            appearance = appearanceInput?.text,
            personality = personalityInput?.text,
            inventory = inventoryInput?.text,
            quote = quoteInput?.text,
            backstory = backstoryInput?.text
        };
    }
}
