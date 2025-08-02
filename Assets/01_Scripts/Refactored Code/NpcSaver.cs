using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using SFB;
using System;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class NpcSaver : MonoBehaviour
{
    public NpcHistoryPanel historyPanel;

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

    private void Start()
    {
        DebugFontFileCheck();
    }

    public void DebugFontFileCheck()
    {
        string fontPath = Path.Combine(Application.streamingAssetsPath, "FantaisieArtistique.ttf");
        string logPath = Path.Combine(Application.persistentDataPath, "font_debug_log.txt");
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Font path: " + fontPath);

        if (File.Exists(fontPath))
        {
            long size = new FileInfo(fontPath).Length;
            string message = $"Font file exists! Size: {size} bytes";
            Debug.Log(message);
            sb.AppendLine(message);
        }
        else
        {
            string message = "Font file NOT found!";
            Debug.LogError(message);
            sb.AppendLine(message);
        }

        try
        {
            File.WriteAllText(logPath, sb.ToString());
            Debug.Log("Debug log saved to: " + logPath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to write debug log: " + ex.Message);
        }
    }


    public void SaveNpcWithFileDialog()
    {
        var npc = CollectNpcData();

        var extensions = new[]
        {
            new ExtensionFilter("PDF Files", "pdf"),
            new ExtensionFilter("All Files", "*")
        };

        string cleanName = nameInput?.text?.Trim();
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        string defaultName = string.IsNullOrWhiteSpace(cleanName)
            ? $"npc_{timestamp}"
            : $"npc_{SanitizeFileName(cleanName)}_{DateTime.Now:yyyyMMdd}";

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string path = StandaloneFileBrowser.SaveFilePanel("Save NPC", desktopPath, defaultName, extensions);

        if (!string.IsNullOrEmpty(path))
        {
            if (!path.EndsWith(".pdf"))
            {
                path += ".pdf"; //Force .pdf extension if omitted
            }
            SaveNpcAsPdf(npc, path);
        }
    }

    private void SaveNpcAsPdf(NpcData npc, string path)
    {
        try
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var doc = new Document(PageSize.A4, 80f, 80f, 185f, 100f);
                var writer = PdfWriter.GetInstance(doc, fs);

                string backgroundPath = Path.Combine(Application.streamingAssetsPath, "PDFBackground.png");
                Debug.Log($"Loading background: {backgroundPath}");

                if (File.Exists(backgroundPath))
                {
                    writer.PageEvent = new ScrollBackground(backgroundPath);
                }
                else
                {
                    Debug.LogWarning("PDF background not found.");
                }

                doc.Open();

                string fontPath = Path.Combine(Application.streamingAssetsPath, "GreatVibes-Regular.ttf");
                iTextSharp.text.Font fancyFont;

                if (File.Exists(fontPath))
                {
                    try
                    {
                        BaseFont baseFont = BaseFont.CreateFont(
                            fontPath,
                            BaseFont.IDENTITY_H,
                            BaseFont.EMBEDDED
                        );
                        fancyFont = new iTextSharp.text.Font(baseFont, 19);
                        Debug.Log("Custom font loaded: Great Vibes");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Failed to load Great Vibes: " + ex);
                        fancyFont = FontFactory.GetFont("Arial", 19, iTextSharp.text.Font.NORMAL);
                    }
                }
                else
                {
                    Debug.LogWarning("Great Vibes font not found, using Arial.");
                    fancyFont = FontFactory.GetFont("Arial", 19, iTextSharp.text.Font.NORMAL);
                }


                void AddPdfParagraph(string label, string value)
                {
                    value = CleanText(value);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        try
                        {
                            doc.Add(new Paragraph($"{label}: {value}", fancyFont));
                        }
                        catch (DocumentException ex)
                        {
                            Debug.LogWarning($"Could not add paragraph '{label}': {ex.Message}");
                        }
                    }
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
            Debug.LogError($"PDF save failed: {ex}");
        }
    }

    private string CleanText(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        input = input.Replace("\u200B", "");     //Zero-width space
        input = input.Replace("\u25A1", "[ ]");  //White square fallback
        return input;
    }

    private string SanitizeFileName(string input)
    {
        var invalids = Path.GetInvalidFileNameChars();
        return string.Join("_", input.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).Trim();
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

    public void SaveAsJson()
    {
        var npc = CollectNpcData();

        string saveFolder = Path.Combine(Application.persistentDataPath, "NPCs");
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        string fileName = $"npc_{System.DateTime.Now:yyyyMMdd_HHmmss}.json";
        string fullPath = Path.Combine(saveFolder, fileName);

        string json = JsonUtility.ToJson(npc, true);
        File.WriteAllText(fullPath, json);
        Debug.Log($"NPC saved to: {fullPath}");
    }

    public IEnumerator SaveJsonAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SaveAsJson();
        if (historyPanel != null)
        {
            historyPanel.LoadRecentNpcButtons();
        }
    }
}
