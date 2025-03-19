using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DiceRollerPhysics : MonoBehaviour
{
    [SerializeField] private GameObject _D4_Object;
    [SerializeField] private GameObject _D6_Object;
    [SerializeField] private GameObject _D8_Object;
    [SerializeField] private GameObject _D10_Object;
    [SerializeField] private GameObject _D12_Object;
    [SerializeField] private GameObject _D20_Object;
    [SerializeField] private GameObject _D100_Object;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float rollForce = 5f;
    [SerializeField] private TMP_Text diceChoiceText;

    private Dictionary<int, GameObject> dicePrefabs;
    private Dictionary<int, int> selectedDice = new Dictionary<int, int>();
    private List<GameObject> spawnedDice = new List<GameObject>();
    private const int maxDicePerType = 20;

    void Start()
    {
        dicePrefabs = new Dictionary<int, GameObject>
        {
            { 4, _D4_Object },
            { 6, _D6_Object },
            { 8, _D8_Object },
            { 10, _D10_Object },
            { 12, _D12_Object },
            { 20, _D20_Object },
            { 100, _D100_Object }
        };

        UpdateDiceChoiceText();
    }

    public void AddDice(int sides)
    {
        if (selectedDice.ContainsKey(sides))
        {
            if (selectedDice[sides] < maxDicePerType)
                selectedDice[sides]++;
        }
        else
        {
            selectedDice[sides] = 1;
        }

        UpdateDiceChoiceText();
    }

    public void RemoveDice(int sides)
    {
        if (selectedDice.ContainsKey(sides))
        {
            selectedDice[sides]--;
            if (selectedDice[sides] <= 0)
                selectedDice.Remove(sides);
        }

        UpdateDiceChoiceText();
    }

    public void ClearDice()
    {
        selectedDice.Clear();
        ClearSpawnedDice();
        UpdateDiceChoiceText();
    }

    private void ClearSpawnedDice()
    {
        foreach (GameObject dice in spawnedDice)
        {
            Destroy(dice);
        }
        spawnedDice.Clear();
    }

    private void UpdateDiceChoiceText()
    {
        List<string> diceList = new List<string>();
        foreach (var dice in selectedDice)
            diceList.Add($"{dice.Value} x D{dice.Key}");

        diceChoiceText.text = diceList.Count > 0 ? "Rolling: " + string.Join(", ", diceList) : "No dice selected.";
    }

    public void RollSelectedDice()
    {
        ClearSpawnedDice();
        foreach (var dice in selectedDice)
            RollDice(dice.Value, dice.Key);

        StartCoroutine(GetDiceResults());
    }

    private void RollDice(int count, int sides)
    {
        if (!dicePrefabs.ContainsKey(sides)) return;

        for (int i = 0; i < count; i++)
        {
            GameObject dice = Instantiate(dicePrefabs[sides], spawnPoint.position, Random.rotation);
            spawnedDice.Add(dice);
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 force = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)) * rollForce;
                rb.AddForce(force, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * rollForce, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator GetDiceResults()
    {
        yield return new WaitForSeconds(2f); // Wait for dice to settle

        foreach (GameObject dice in spawnedDice)
        {
            DiceFaceDetector detector = dice.GetComponent<DiceFaceDetector>();
            if (detector != null)
            {
                int result = detector.GetTopFace();
                Debug.Log($"Rolled a D{detector.faces.Length}, result: {result}");
            }
        }
    }
}