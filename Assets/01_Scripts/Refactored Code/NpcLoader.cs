using UnityEngine;
using TMPro;

public class NpcLoader : MonoBehaviour
{
    public GameObject generatorPanel;

    public TMP_InputField nameInput, descriptionInput, plotHookInput;
    public TMP_InputField occupationInput, raceInput, alignmentInput, statsInput;
    public TMP_InputField appearanceInput, personalityInput, inventoryInput;
    public TMP_InputField quoteInput, backstoryInput;

    public MenuManager menuManager;

    public GeneratorSetup genSetup;

    public GameObject setupPanel;

    public void LoadNpc(NpcApiClient.NpcData npc)
    {
        // Fill input fields
        nameInput.text = npc.name;
        raceInput.text = npc.race;
        alignmentInput.text = npc.alignment;
        statsInput.text = npc.stats;
        descriptionInput.text = npc.description;
        plotHookInput.text = npc.plot_hook;
        occupationInput.text = npc.occupation;
        appearanceInput.text = npc.appearance;
        personalityInput.text = npc.personality;
        inventoryInput.text = npc.inventory;
        quoteInput.text = npc.quote;
        backstoryInput.text = npc.backstory;

        // Apply field toggles before showing generator
        genSetup.ApplyFieldTogglesFromNpc(npc);

        setupPanel.SetActive(false);

        // Switch to the generator panel
        ShowGeneratorPanel();
    }

    public void ShowGeneratorPanel()
    {
        menuManager.ShowOnly("GeneratorPanel");
    }
}
