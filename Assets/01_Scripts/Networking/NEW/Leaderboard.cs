using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private string url = "https://studenthome.hku.nl/~bradley.vanewijk/statistics.php";
    public TextMeshProUGUI leaderboardText;

    void Start()
    {
        StartCoroutine(GetLeaderboard());
    }

    IEnumerator GetLeaderboard()
    {
        Debug.Log("Attempting to get leaderboard from: " + url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log("Received JSON: " + json);
            Player[] players = JsonHelper.FromJson<Player>(json);

            DisplayLeaderboard(players);
        }
    }

    void DisplayLeaderboard(Player[] players)
    {
        leaderboardText.text = "";
        for (int i = 0; i < players.Length; i++)
        {
            Player player = players[i];
            leaderboardText.text += $"<b>{i + 1}.</b> {player.nickname} - {player.points}\n";
        }
    }
}

[System.Serializable]
public class Player
{
    public string nickname;
    public int points;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{\"Items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
