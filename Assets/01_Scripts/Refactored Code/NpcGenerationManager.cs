using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using SFB;
using System;
using System.Collections;
using UnityEngine.UI;

public class NpcGenerationManager : MonoBehaviour
{
    public NpcApiClient apiClient;
    public LocalNpcGenerator localGenerator;
    public NpcSaver npcSaver;
    public GeneratorSetup generatorSetup;
    public NameGeneratorUI nameGenerator;
    public float saveDelaySeconds = 1f;

    public Button _button;

    public void GenerateNpc()
    {
        // 1. Generate local fields
        localGenerator.GenerateAll();
        nameGenerator.GenerateName();

        // 2. Generate API-based fields
        string fields = BuildRequestedFields();
        apiClient.RequestNpcData(fields);

        // 2.5 Disable Generate Button for x amount of seconds
        StartCoroutine(DisableButtonTemporarily(_button, 3));

        // 3. Save after delay (so the inputs are filled first)
        StartCoroutine(npcSaver.SaveJsonAfterDelay(saveDelaySeconds));
    }

    private string BuildRequestedFields()
    {
        // Determine which API fields to request based on active toggles
        var requested = new System.Collections.Generic.List<string>();

        if (generatorSetup.descriptionToggle.isOn) requested.Add("description");
        if (generatorSetup.plotHookToggle.isOn) requested.Add("plot_hook");
        if (generatorSetup.occupationToggle.isOn) requested.Add("occupation");
        if (generatorSetup.appearanceToggle.isOn) requested.Add("appearance");
        if (generatorSetup.personalityToggle.isOn) requested.Add("personality");
        if (generatorSetup.inventoryToggle.isOn) requested.Add("inventory");
        if (generatorSetup.quoteToggle.isOn) requested.Add("quote");
        if (generatorSetup.backstoryToggle.isOn) requested.Add("backstory");

        return string.Join(",", requested);
    }

    private IEnumerator DisableButtonTemporarily(Button button, float seconds)
    {
        button.interactable = false;
        yield return new WaitForSeconds(seconds);
        button.interactable = true;
    }
}
