using UnityEngine;
using System.IO;
using SFB; // Standalone File Browser
using UnityEngine.UI;

public class NPCFileSaver : MonoBehaviour
{
    [Header("References")]
    public InputField nameField;
    public InputField descriptionField;
    public InputField plotHookField;

    public void SaveNPC()
    {
        string npcText = $"Name: {nameField.text}\nDescription: {descriptionField.text}\nPlot Hook: {plotHookField.text}";

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
