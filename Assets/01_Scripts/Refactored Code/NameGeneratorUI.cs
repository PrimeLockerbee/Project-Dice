using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameGeneratorUI : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextAsset trainingData; // e.g., a text file with fantasy names

    private MarkovGenerator generator;

    public Toggle lockNameToggle;

    public GeneratorSetup generatorSetup; // Assign in inspector

    private void Start()
    {
        var names = trainingData.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        generator = new MarkovGenerator(order: 2);
        generator.Train(names);
    }

    public void GenerateName()
    {
        if (generator == null) return;

        // Only generate if name toggle is ON in the setup
        if (!generatorSetup.nameToggle.isOn)
        {
            nameInputField.text = ""; // Optionally clear if not generating
            return;
        }

        string name = generator.Generate(10);

        if (!lockNameToggle.isOn)
            nameInputField.text = name;
    }

}
