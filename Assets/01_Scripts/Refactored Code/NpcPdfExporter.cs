using UnityEngine;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SFB; // For StandaloneFileBrowser
using TMPro;

public class NpcPdfExporter : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField nameInput, descriptionInput, plotHookInput, occupationInput, raceInput;
    public TMP_InputField alignmentInput, statsInput, appearanceInput, personalityInput;
    public TMP_InputField inventoryInput, quoteInput, backstoryInput;

    public void SaveNpcAsPdf()
    {
        string path = StandaloneFileBrowser.SaveFilePanel("Save NPC as PDF", "", $"npc_{System.DateTime.Now:yyyyMMdd_HHmmss}.pdf", "pdf");

        if (string.IsNullOrEmpty(path)) return;

        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var boldFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12);
                var normalFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12);


                AddField(doc, "Name", nameInput.text, boldFont, normalFont);
                AddField(doc, "Description", descriptionInput.text, boldFont, normalFont);
                AddField(doc, "Plot Hook", plotHookInput.text, boldFont, normalFont);
                AddField(doc, "Occupation", occupationInput.text, boldFont, normalFont);
                AddField(doc, "Race", raceInput.text, boldFont, normalFont);
                AddField(doc, "Alignment", alignmentInput.text, boldFont, normalFont);
                AddField(doc, "Stats", statsInput.text, boldFont, normalFont);
                AddField(doc, "Appearance", appearanceInput.text, boldFont, normalFont);
                AddField(doc, "Personality", personalityInput.text, boldFont, normalFont);
                AddField(doc, "Inventory", inventoryInput.text, boldFont, normalFont);
                AddField(doc, "Quote", quoteInput.text, boldFont, normalFont);
                AddField(doc, "Backstory", backstoryInput.text, boldFont, normalFont);

                doc.Close();
                writer.Close();
            }

            Debug.Log($"NPC exported as PDF to: {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError("PDF generation failed: " + e.Message);
        }
    }

    void AddField(Document doc, string title, string content, iTextSharp.text.Font titleFont, iTextSharp.text.Font contentFont)

    {
        if (!string.IsNullOrWhiteSpace(content))
        {
            doc.Add(new Paragraph($"{title}:", titleFont));
            doc.Add(new Paragraph(content + "\n", contentFont));
        }
    }
}
