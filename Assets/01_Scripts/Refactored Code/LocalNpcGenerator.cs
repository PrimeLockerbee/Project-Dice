using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // For Toggle

public class LocalNpcGenerator : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField raceInput;
    public TMP_InputField alignmentInput;
    public TMP_InputField statsInput;

    [Header("Lock Toggles")]
    public Toggle lockRaceToggle;
    public Toggle lockAlignmentToggle;
    public Toggle lockStatsToggle;

    private List<string> races = new List<string>
    {
        "Human", "Elf", "Dwarf", "Orc", "Halfling", "Gnome", "Dragonborn", "Tiefling"
    };

    private List<string> alignments = new List<string>
    {
        "Lawful Good", "Neutral Good", "Chaotic Good",
        "Lawful Neutral", "True Neutral", "Chaotic Neutral",
        "Lawful Evil", "Neutral Evil", "Chaotic Evil"
    };

    public void GenerateRace()
    {
        if (lockRaceToggle == null || !lockRaceToggle.isOn)
        {
            raceInput.text = races[Random.Range(0, races.Count)];
        }
    }

    public void GenerateAlignment()
    {
        if (lockAlignmentToggle == null || !lockAlignmentToggle.isOn)
        {
            alignmentInput.text = alignments[Random.Range(0, alignments.Count)];
        }
    }

    public void GenerateStats()
    {
        if (lockStatsToggle == null || !lockStatsToggle.isOn)
        {
            int[] stats = new int[6];
            for (int i = 0; i < 6; i++)
            {
                stats[i] = Roll3d6();
            }
            statsInput.text = $"STR: {stats[0]} DEX: {stats[1]} CON: {stats[2]} INT: {stats[3]} WIS: {stats[4]} CHA: {stats[5]}";
        }
    }

    private int Roll3d6()
    {
        return Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7);
    }

    public void GenerateAll()
    {
        GenerateRace();
        GenerateAlignment();
        GenerateStats();
    }
}
