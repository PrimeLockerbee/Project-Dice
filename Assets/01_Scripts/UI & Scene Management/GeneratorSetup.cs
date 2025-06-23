using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GeneratorSetup : MonoBehaviour
{
    [Header("Controls")]
    public Button continueButton;

    [Header("Toggle Inputs")]
    public Toggle nameToggle, descriptionToggle, plotHookToggle, statsToggle;
    public Toggle occupationToggle, raceToggle, alignmentToggle;
    public Toggle appearanceToggle, personalityToggle, inventoryToggle;
    public Toggle quoteToggle, backstoryToggle;

    [Header("InputFields")]
    public GameObject nameInputField, descriptionInputField, plotHookInputField, statsInputField;
    public GameObject occupationInputField, raceInputField, alignmentInputField;
    public GameObject appearanceInputField, personalityInputField, inventoryInputField;
    public GameObject quoteInputField, backstoryInputField;

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

        foreach (var toggle in toggleToInputField.Keys)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((isOn) =>
            {
                toggleToInputField[toggle].SetActive(isOn);
                UpdateContinueButton();
            });

            toggleToInputField[toggle].SetActive(toggle.isOn);
        }

        UpdateContinueButton();
    }

    public void OnContinue()
    {
        setupPanel.SetActive(false);
        generatorPanel.SetActive(true);

        foreach (var pair in toggleToInputField)
        {
            pair.Value.SetActive(pair.Key.isOn);
        }
    }

    void UpdateContinueButton()
    {
        bool anyOn = toggleToInputField.Keys.Any(t => t.isOn);
        continueButton.interactable = anyOn;
    }

    public void ApplyFieldTogglesFromNpc(NpcData npc)
    {
        var fields = new HashSet<string>(npc.generatedFields);

        nameToggle.isOn = fields.Contains("name");
        descriptionToggle.isOn = fields.Contains("description");
        plotHookToggle.isOn = fields.Contains("plot_hook");
        occupationToggle.isOn = fields.Contains("occupation");
        raceToggle.isOn = fields.Contains("race");
        alignmentToggle.isOn = fields.Contains("alignment");
        statsToggle.isOn = fields.Contains("stats");
        appearanceToggle.isOn = fields.Contains("appearance");
        personalityToggle.isOn = fields.Contains("personality");
        inventoryToggle.isOn = fields.Contains("inventory");
        quoteToggle.isOn = fields.Contains("quote");
        backstoryToggle.isOn = fields.Contains("backstory");
    }
}
