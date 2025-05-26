using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GeneratorSetup : MonoBehaviour
{
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

        // Make sure the generator panel is hidden at start
        generatorPanel.SetActive(false);
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
}
