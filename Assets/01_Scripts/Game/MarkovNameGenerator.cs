using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MarkovNameGenerator : MonoBehaviour
{
    [Tooltip("Seed names to learn from")]
    [TextArea(5, 20)]
    public string seedNamesText;

    private Dictionary<string, List<char>> markovChain = new Dictionary<string, List<char>>();
    private System.Random random = new System.Random();
    private HashSet<string> generatedNames = new HashSet<string>();

    public int order = 2; //Number of characters for the key (2 is a good start)
    public int minLength = 4;
    public int maxLength = 10;

    void Start()
    {
        BuildMarkovChain(seedNamesText);
    }

    //Build the Markov chain dictionary
    void BuildMarkovChain(string inputText)
    {
        markovChain.Clear();

        //Split input names by new lines
        string[] names = inputText.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var name in names)
        {
            string paddedName = "^" + name.ToLower() + "$"; //^ and $ as start/end markers
            for (int i = 0; i <= paddedName.Length - order; i++)
            {
                string key = paddedName.Substring(i, order);
                char nextChar = (i + order < paddedName.Length) ? paddedName[i + order] : '$';

                if (!markovChain.ContainsKey(key))
                    markovChain[key] = new List<char>();

                markovChain[key].Add(nextChar);
            }
        }
    }

    public string GenerateName()
    {
        int attempts = 0;
        while (attempts < 100)
        {
            StringBuilder nameBuilder = new StringBuilder();

            //Start with ^ plus random key starting with ^
            List<string> possibleStarts = new List<string>();
            foreach (var key in markovChain.Keys)
                if (key.StartsWith("^")) possibleStarts.Add(key);

            if (possibleStarts.Count == 0)
            {
                Debug.LogError("No valid start keys in Markov chain.");
                return "";
            }

            string currentKey = possibleStarts[random.Next(possibleStarts.Count)];
            nameBuilder.Append(currentKey.Substring(1)); //Skip the ^

            while (true)
            {
                if (!markovChain.ContainsKey(currentKey))
                    break;

                List<char> possibleNextChars = markovChain[currentKey];
                char nextChar = possibleNextChars[random.Next(possibleNextChars.Count)];

                if (nextChar == '$')
                    break;

                nameBuilder.Append(nextChar);
                //Shift key by one char and append nextChar
                currentKey = currentKey.Substring(1) + nextChar;

                if (nameBuilder.Length >= maxLength)
                    break;
            }

            string generated = Capitalize(nameBuilder.ToString());

            if (generated.Length >= minLength && !generatedNames.Contains(generated))
            {
                generatedNames.Add(generated);
                return generated;
            }

            attempts++;
        }

        return "NoUniqueName"; //Fallback if no unique found after attempts
    }

    private string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
