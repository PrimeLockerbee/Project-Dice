using System;
using System.Collections.Generic;
using System.Text;

public class MarkovGenerator
{
    private Dictionary<string, List<char>> chain = new Dictionary<string, List<char>>();
    private int order;
    private Random rng;

    public MarkovGenerator(int order = 2, int? seed = null)
    {
        this.order = order;
        rng = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    //Builds the Markov chain based on input names
    public void Train(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            //Pad with underscores for start and end markers
            string padded = new string('_', order) + name + "_";

            for (int i = 0; i <= padded.Length - order - 1; i++)
            {
                string key = padded.Substring(i, order);
                char nextChar = padded[i + order];

                if (!chain.ContainsKey(key))
                    chain[key] = new List<char>();

                chain[key].Add(nextChar);
            }
        }
    }

    //Generates a new name based on the trained Markov chain
    public string Generate(int maxLength = 10)
    {
        string current = new string('_', order); //Start with padding
        StringBuilder result = new StringBuilder();

        while (result.Length < maxLength)
        {
            if (!chain.ContainsKey(current)) break;

            var options = chain[current];
            char next = options[rng.Next(options.Count)];
            if (next == '_') break; //End of name

            result.Append(next);
            current = current.Substring(1) + next; // shift window
        }

        return Capitalize(result.ToString());
    }

    //Capitalizes the first letter of the generated name
    private string Capitalize(string input) =>
        string.IsNullOrEmpty(input) ? "" : char.ToUpper(input[0]) + input.Substring(1);
}
