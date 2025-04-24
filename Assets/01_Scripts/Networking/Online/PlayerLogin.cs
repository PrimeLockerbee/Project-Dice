using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLogin : MonoBehaviour
{
    private string userLoginURL = "https://studenthome.hku.nl/~bradley.vanewijk/user_login.php";
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;

    [SerializeField] private GameObject accesPanel;

    public void Login()
    {
        string email = emailInputField.text; // Retrieve the email value from the InputField
        string password = passwordInputField.text; // Retrieve the password value from the InputField

        StartCoroutine(PlayerLoginRequest(email, password));
    }

    private IEnumerator PlayerLoginRequest(string email, string password)
    {
        // Create a form to hold the login data
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        // Send the POST request to the player login script
        using (UnityWebRequest www = UnityWebRequest.Post(userLoginURL, form))
        {
            yield return www.SendWebRequest();

            // Check for errors during the request
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Player login request failed. Error: " + www.error);
            }
            else
            {
                // Get the response from the server
                string response = www.downloadHandler.text;

                // Check if the response contains the username
                if (!string.IsNullOrEmpty(response) && !response.StartsWith("Invalid"))
                {
                    Debug.Log("Player Login successful");

                    // Assuming the response string contains the id, nickname, and session id separated by newlines
                    string[] lines = response.Split('\n');

                    if (lines.Length >= 3)
                    {
                        string id = lines[0];
                        string nickname = lines[1];
                        string sessionId = lines[2];


                        // Store the variables in PlayerPrefs
                        PlayerPrefs.SetString("ID", id);
                        Debug.Log("Player ID stored: " + PlayerPrefs.GetString("ID"));
                        PlayerPrefs.SetString("Nickname", nickname);

                        PlayerPrefs.SetString("SessionID", sessionId);

                        // Now you can use the variables as needed
                        Debug.Log("ID: " + id);
                        Debug.Log("Nickname: " + nickname);
                        Debug.Log("Session ID: " + sessionId);
                    }
                    else
                    {
                        Debug.LogError("Invalid response format");
                    }

                    // Perform any actions required upon successful player login
                    //SceneManager.LoadScene(1);
                    accesPanel.SetActive(true);    

                }
                else
                {
                    Debug.Log("Player login failed");
                    // Perform any actions required upon failed player login
                }
            }
        }
    }
}