using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class NPCGeneratorDATABASEGEN : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField descriptionField;
    public TMP_InputField plotHookField;

    private string url = "lockerbeeprime.x10.mx/get_npc.php"; // change to your server path

    public void GenerateNPC()
    {
        StartCoroutine(GetNPCData());
    }

    IEnumerator GetNPCData()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Parse JSON
            string json = www.downloadHandler.text;
            NPC npc = JsonUtility.FromJson<NPC>(json);

            nameField.text = npc.name;
            descriptionField.text = npc.description;
            plotHookField.text = npc.plothook;
        }
    }

    [System.Serializable]
    public class NPC
    {
        public string name;
        public string description;
        public string plothook;
    }
}
