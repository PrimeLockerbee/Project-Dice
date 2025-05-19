using UnityEngine;
using System.IO;
using SFB; // Standalone File Browser namespace

public class NPCFileSaver : MonoBehaviour
{
    public NPCGeneratorDATABASEGEN npcGenerator;

    public void SaveNPCToTXT()
    {
        string npcText = npcGenerator.CollectAllNPCData();

        // Open a file save dialog
        var path = StandaloneFileBrowser.SaveFilePanel(
            "Save NPC as TXT",    // Title
            "",                   // Default path
            "Generated_NPC",      // Default filename
            "txt"                 // Extension
        );

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, npcText);
            Debug.Log("NPC saved to: " + path);
        }
        else
        {
            Debug.Log("Save cancelled.");
        }
    }
}
