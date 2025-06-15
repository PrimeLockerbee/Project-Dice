using UnityEngine;
using TMPro;

public class NpcCopyToClipboard : MonoBehaviour
{
    [System.Serializable]
    public class NamedInputField
    {
        public string fieldName;
        public TMP_InputField inputField;
    }

    public NamedInputField[] inputFieldsToCopy;

    public void CopyNpcData()
    {
        string combinedText = "";

        foreach (var namedField in inputFieldsToCopy)
        {
            var text = namedField.inputField.text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                combinedText += $"{namedField.fieldName}: {text}\n";
            }
        }

        GUIUtility.systemCopyBuffer = combinedText.Trim();
        Debug.Log("NPC data copied to clipboard:\n" + combinedText);
    }
}
