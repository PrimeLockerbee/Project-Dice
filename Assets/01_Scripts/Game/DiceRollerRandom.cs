using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceRollerRandom : MonoBehaviour
{
    public TMP_Text resultText; // Assign in Unity Editor
    public TMP_Text diceChoiceText; // To show selected dice

    private Dictionary<int, int> diceToRoll = new Dictionary<int, int>();

    public void AddDice(int count, int sides)
    {
        if (diceToRoll.ContainsKey(sides))
        {
            diceToRoll[sides] += count;
        }
        else
        {
            diceToRoll[sides] = count;
        }
        UpdateDiceChoiceText();
    }

    //public void RemoveDice(int count, int sides)
    //{
    //    if (diceToRoll.ContainsKey(sides))
    //    {
    //        diceToRoll[sides] -= count;
    //        if (diceToRoll[sides] <= 0)
    //        {
    //            diceToRoll.Remove(sides);
    //        }
    //    }
    //    UpdateDiceChoiceText();
    //}

    public void SetD4() { AddDice(1, 4); }
    public void SetD6() { AddDice(1, 6); }
    public void SetD8() { AddDice(1, 8); }
    public void SetD10() { AddDice(1, 10); }
    public void SetD12() { AddDice(1, 12); }
    public void SetD20() { AddDice(1, 20); }

    //public void RemoveD4() { RemoveDice(1, 4); }
    //public void RemoveD6() { RemoveDice(1, 6); }
    //public void RemoveD8() { RemoveDice(1, 8); }
    //public void RemoveD10() { RemoveDice(1, 10); }
    //public void RemoveD12() { RemoveDice(1, 12); }
    //public void RemoveD20() { RemoveDice(1, 20); }

    public void ClearDice()
    {
        diceToRoll.Clear();
        UpdateDiceChoiceText();
    }

    private void UpdateDiceChoiceText()
    {
        List<string> formattedDice = new List<string>();
        foreach (var entry in diceToRoll)
        {
            formattedDice.Add($"{entry.Value} x D{entry.Key}");
        }
        diceChoiceText.text = "Rolling: " + string.Join(", ", formattedDice);
    }

    public void RollDice()
    {
        int total = 0;
        List<string> rollsResult = new List<string>();

        foreach (var entry in diceToRoll)
        {
            int count = entry.Value;
            int sides = entry.Key;
            List<int> rolls = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int roll = Random.Range(1, sides + 1);
                rolls.Add(roll);
                total += roll;
            }
            rollsResult.Add($"{count} x D{sides}: {string.Join(", ", rolls)}");
        }

        resultText.text = $"{string.Join(" | ", rollsResult)} (Total: {total})";
    }
}
