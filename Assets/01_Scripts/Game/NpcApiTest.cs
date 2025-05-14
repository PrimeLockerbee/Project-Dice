using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NpcApiTest : MonoBehaviour
{
    // Replace this with the URL of your PHP script
    private string apiUrl = "https://lockerbeeprime.x10.mx/get_npc.php";

    void Start()
    {
        StartCoroutine(GetRandomNpc());
    }

    IEnumerator GetRandomNpc()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.Success)
#else
            if (www.isNetworkError || www.isHttpError)
#endif
            {
                Debug.LogError("API Request Failed: " + www.error);
            }
            else
            {
                Debug.Log("API Request Succeeded!");
                Debug.Log("Response: " + www.downloadHandler.text);
            }
        }
    }
}