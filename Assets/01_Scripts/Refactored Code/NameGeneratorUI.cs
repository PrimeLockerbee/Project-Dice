using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameGeneratorUI : MonoBehaviour
{
    private MarkovGenerator generator;
    public GeneratorSetup generatorSetup;

    public TMP_InputField nameInputField;
    public TextAsset trainingData; //A text file with names to train

    public Toggle lockNameToggle;

    private void Start()
    {
        var names = trainingData.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        generator = new MarkovGenerator(order: 2);
        generator.Train(names);
    }

    public void GenerateName()
    {
        if (generator == null) return;

        //Only generate if name toggle is ON in the setup
        if (!generatorSetup.nameToggle.isOn)
        {
            nameInputField.text = ""; //Optionally clear if not generating
            return;
        }

        string name = generator.Generate(10);

        if (!lockNameToggle.isOn)
            nameInputField.text = name;
    }

}
