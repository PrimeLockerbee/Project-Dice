using UnityEngine;
using System.IO;
using SFB; // Standalone File Browser
using System.Collections.Generic;

public class NPCFileSaver : MonoBehaviour
{
    public NPCGeneratorDATABASEGEN npcGenerator;

    public void SaveNPCToTXT()
    {
        string npcText = npcGenerator.CollectAllNPCData();

        var path = StandaloneFileBrowser.SaveFilePanel(
            "Save NPC as TXT", "", "Generated_NPC", "txt");

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
