using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GeneratorSetup : MonoBehaviour
{
    [Header("Controls")]
    public Button continueButton;

    [Header("Toggle Inputs (Setup Panel)")]
    public Toggle nameToggle;
    public Toggle descriptionToggle;
    public Toggle plotHookToggle;
    public Toggle statsToggle;
    public Toggle occupationToggle;
    public Toggle raceToggle;
    public Toggle alignmentToggle;
    public Toggle appearanceToggle;
    public Toggle personalityToggle;
    public Toggle inventoryToggle;
    public Toggle quoteToggle;
    public Toggle backstoryToggle;

    [Header("InputFields (Generator Panel)")]
    public GameObject nameInputField;
    public GameObject descriptionInputField;
    public GameObject plotHookInputField;
    public GameObject statsInputField;
    public GameObject occupationInputField;
    public GameObject raceInputField;
    public GameObject alignmentInputField;
    public GameObject appearanceInputField;
    public GameObject personalityInputField;
    public GameObject inventoryInputField;
    public GameObject quoteInputField;
    public GameObject backstoryInputField;

    [Header("Panels")]
    public GameObject setupPanel;
    public GameObject generatorPanel;

    private Dictionary<Toggle, GameObject> toggleToInputField;

    void Start()
    {
        toggleToInputField = new Dictionary<Toggle, GameObject> {
        { nameToggle, nameInputField },
        { descriptionToggle, descriptionInputField },
        { plotHookToggle, plotHookInputField },
        { statsToggle, statsInputField },
        { occupationToggle, occupationInputField },
        { raceToggle, raceInputField },
        { alignmentToggle, alignmentInputField },
        { appearanceToggle, appearanceInputField },
        { personalityToggle, personalityInputField },
        { inventoryToggle, inventoryInputField },
        { quoteToggle, quoteInputField },
        { backstoryToggle, backstoryInputField }
        };

        // Clear any previous listeners to avoid duplicates
        foreach (var toggle in toggleToInputField.Keys)
        {
            toggle.onValueChanged.RemoveAllListeners();

            toggle.onValueChanged.AddListener((isOn) =>
            {
                // Show or hide the linked input field based on toggle
                toggleToInputField[toggle].SetActive(isOn);

                // Update the continue button's interactability
                UpdateContinueButton();
            });

            // Initialize input field visibility based on toggle state
            toggleToInputField[toggle].SetActive(toggle.isOn);
        }

        // Set continue button interactability on startup
        UpdateContinueButton();
    }


    public void OnContinue()
    {
        // Hide setup panel
        setupPanel.SetActive(false);

        // Show generator panel
        generatorPanel.SetActive(true);

        // Enable or disable InputFields based on toggle state
        foreach (var pair in toggleToInputField)
        {
            pair.Value.SetActive(pair.Key.isOn);
        }
    }

    void UpdateContinueButton()
    {
        bool anyToggleOn = false;
        foreach (var toggle in toggleToInputField.Keys)
        {
            if (toggle.isOn)
            {
                anyToggleOn = true;
                break;
            }
        }
        continueButton.interactable = anyToggleOn;
    }

    public void ApplyFieldTogglesFromNpc(NpcApiClient.NpcData npc)
    {
        nameToggle.isOn = !string.IsNullOrWhiteSpace(npc.name);
        descriptionToggle.isOn = !string.IsNullOrWhiteSpace(npc.description);
        plotHookToggle.isOn = !string.IsNullOrWhiteSpace(npc.plot_hook);
        statsToggle.isOn = !string.IsNullOrWhiteSpace(npc.stats);
        occupationToggle.isOn = !string.IsNullOrWhiteSpace(npc.occupation);
        raceToggle.isOn = !string.IsNullOrWhiteSpace(npc.race);
        alignmentToggle.isOn = !string.IsNullOrWhiteSpace(npc.alignment);
        appearanceToggle.isOn = !string.IsNullOrWhiteSpace(npc.appearance);
        personalityToggle.isOn = !string.IsNullOrWhiteSpace(npc.personality);
        inventoryToggle.isOn = !string.IsNullOrWhiteSpace(npc.inventory);
        quoteToggle.isOn = !string.IsNullOrWhiteSpace(npc.quote);
        backstoryToggle.isOn = !string.IsNullOrWhiteSpace(npc.backstory);

        // Force-refresh input fields visibility
        foreach (var pair in toggleToInputField)
        {
            pair.Value.SetActive(pair.Key.isOn);
        }

        // Update continue button just in case
        UpdateContinueButton();
    }

}