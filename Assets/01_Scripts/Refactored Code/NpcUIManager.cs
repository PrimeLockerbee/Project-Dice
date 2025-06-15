using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcUIManager : MonoBehaviour
{
    [Header("UI Fields")]
    public TMP_InputField descriptionInput;
    public TMP_InputField plotHookInput;
    public TMP_InputField occupationInput;
    public TMP_InputField appearanceInput;
    public TMP_InputField personalityInput;
    public TMP_InputField inventoryInput;
    public TMP_InputField quoteInput;
    public TMP_InputField backstoryInput;

    [Header("Lock Toggles")]
    public Toggle lockDescriptionToggle;
    public Toggle lockPlotHookToggle;
    public Toggle lockOccupationToggle;
    public Toggle lockAppearanceToggle;
    public Toggle lockPersonalityToggle;
    public Toggle lockInventoryToggle;
    public Toggle lockQuoteToggle;
    public Toggle lockBackstoryToggle;

    public NpcApiClient npcApiClient;

    private void OnEnable()
    {
        if (npcApiClient != null)
        {
            npcApiClient.OnNpcDataReceived += UpdateUI;
        }
    }

    private void OnDisable()
    {
        if (npcApiClient != null)
        {
            npcApiClient.OnNpcDataReceived -= UpdateUI;
        }
    }

    private void UpdateUI(NpcApiClient.NpcData npc)
    {
        if (!lockDescriptionToggle.isOn)
            descriptionInput.text = npc.description;
        if (!lockPlotHookToggle.isOn)
            plotHookInput.text = npc.plot_hook;
        if (!lockOccupationToggle.isOn)
            occupationInput.text = npc.occupation;
        if (!lockAppearanceToggle.isOn)
            appearanceInput.text = npc.appearance;
        if (!lockPersonalityToggle.isOn)
            personalityInput.text = npc.personality;
        if (!lockInventoryToggle.isOn)
            inventoryInput.text = npc.inventory;
        if (!lockQuoteToggle.isOn)
            quoteInput.text = npc.quote;
        if (!lockBackstoryToggle.isOn)
            backstoryInput.text = npc.backstory;
    }

    public void RequestNewNpc()
    {
        npcApiClient.RequestNpcData("description,plot_hook,occupation,appearance,personality,inventory,quote,backstory");
    }
}
