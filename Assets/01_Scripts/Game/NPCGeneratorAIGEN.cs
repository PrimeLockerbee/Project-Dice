using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Text;
using Newtonsoft.Json;

public class NPCGeneratorOpenRouter : MonoBehaviour
{
    public TMP_Text outputText; // TMP_Text component to display the success message

    private string apiKey = "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"; // Replace with your OpenRouter API Key
    private string apiUrl = "https://openrouter.ai/api/v1/chat/completions"; // Correct OpenRouter endpoint

    public void GenerateNPC()
    {
        string prompt = "Generate a random NPC with a name, description, and plot hook.";

        // Start the request to OpenRouter API
        StartCoroutine(SendOpenRouterRequest(prompt));
    }

    IEnumerator SendOpenRouterRequest(string prompt)
    {
        var requestData = new
        {
            model = "openai/gpt-4o", // Optional model, specify if needed
            messages = new[] {
                new { role = "user", content = prompt }
            },
            max_tokens = 1000 // Set a limit to avoid exceeding token quota
        };

        // Convert data to JSON using Newtonsoft.Json
        string jsonData = JsonConvert.SerializeObject(requestData);
        Debug.Log("Request JSON: " + jsonData);  // Log the JSON to see its format

        // Send the request
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

            // Wait for the request to finish
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("OpenRouter API error: " + request.error);
                outputText.text = "Error: " + request.error;  // Display error in the TMP_Text field
            }
            else
            {
                // Log the result for debugging
                Debug.Log("Request Success! Response: " + request.downloadHandler.text);

                // Display a simple success message in TMP_Text
                outputText.text = "NPC Generated Successfully!";
            }
        }
    }
}
