using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using SFB;
using System;
using System.Collections;

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

    public NpcHistoryPanel historyPanel;

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
    public void SaveNpcWithFileDialog()
    {
        var npc = CollectNpcData();

        var extensions = new[] {
        new ExtensionFilter("Text or PDF", "txt", "pdf"),
        new ExtensionFilter("Text Files", "txt"),
        new ExtensionFilter("PDF Files", "pdf"),
        new ExtensionFilter("All Files", "*")
    };

        string defaultName = $"npc_{System.DateTime.Now:yyyyMMdd_HHmmss}";
        string path = StandaloneFileBrowser.SaveFilePanel("Save NPC", saveFolder, defaultName, extensions);

        if (!string.IsNullOrEmpty(path))
        {
            if (path.EndsWith(".txt"))
            {
                SaveNpcAsTxt(npc, path);
            }
            else if (path.EndsWith(".pdf"))
            {
                SaveNpcAsPdf(npc, path); // ⬅️ You'll define this in step 2
            }
            else
            {
                Debug.LogWarning("Unsupported file type.");
            }
        }
    }

    private void SaveNpcAsTxt(NpcData npc, string path)
    {
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

        File.WriteAllText(path, sb.ToString());
        Debug.Log($"NPC saved as TXT to: {path}");
    }

    private void SaveNpcAsPdf(NpcData npc, string path)
    {
        // This assumes you’ve imported iTextSharp or another PDF library correctly
        // Replace this with your own working implementation
        try
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var doc = new iTextSharp.text.Document();
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL);

                void AddPdfParagraph(string label, string value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        doc.Add(new iTextSharp.text.Paragraph($"{label}: {value}", font));
                }

                AddPdfParagraph("Name", npc.name);
                AddPdfParagraph("Description", npc.description);
                AddPdfParagraph("Plot Hook", npc.plot_hook);
                AddPdfParagraph("Occupation", npc.occupation);
                AddPdfParagraph("Race", npc.race);
                AddPdfParagraph("Alignment", npc.alignment);
                AddPdfParagraph("Stats", npc.stats);
                AddPdfParagraph("Appearance", npc.appearance);
                AddPdfParagraph("Personality", npc.personality);
                AddPdfParagraph("Inventory", npc.inventory);
                AddPdfParagraph("Quote", npc.quote);
                AddPdfParagraph("Backstory", npc.backstory);

                doc.Close();
                Debug.Log($"NPC saved as PDF to: {path}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"PDF save failed: {ex.Message}");
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

    // Call this from outside to save JSON after a delay (e.g. after generation)
    public IEnumerator SaveJsonAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SaveAsJson();
        historyPanel.LoadRecentNpcButtons();
    }

}
