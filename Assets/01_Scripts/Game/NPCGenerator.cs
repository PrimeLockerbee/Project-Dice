using UnityEngine;
using TMPro;

public class NPCGenerator : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField descriptionField;
    public TMP_InputField plotHookField;

    private string[] names = { "Arin", "Belra", "Caelum", "Doran", "Elira", "Fenric" };
    private string[] descriptions = {
        "A grizzled veteran with a haunted past.",
        "A cheerful merchant always looking for a deal.",
        "A secretive mage with a mysterious agenda.",
        "A young noble running from their duties.",
        "An ex-bandit trying to turn their life around.",
        "A healer with a dark secret."
    };

    private string[] plotHooks = {
        "Is searching for a stolen heirloom.",
        "Is secretly working for a rival faction.",
        "Has information about a hidden dungeon.",
        "Wants revenge on a corrupt official.",
        "Is being blackmailed by a powerful figure.",
        "Needs help finding a missing sibling."
    };

    public void GenerateNPC()
    {
        string newName = names[Random.Range(0, names.Length)];
        string newDescription = descriptions[Random.Range(0, descriptions.Length)];
        string newPlotHook = plotHooks[Random.Range(0, plotHooks.Length)];

        nameField.text = newName;
        descriptionField.text = newDescription;
        plotHookField.text = newPlotHook;

        Debug.Log($"Generated NPC: {newName}, {newDescription}, Hook: {newPlotHook}");
    }
}
