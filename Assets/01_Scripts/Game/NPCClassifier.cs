using UnityEngine;
using Unity.Sentis;
using System;
using System.Collections;
using System.Collections.Generic;

public class NPCClassifier : MonoBehaviour
{
    public ModelAsset modelAsset;
    private Model runtimeModel;
    private Worker worker;

    void Start()
    {
        runtimeModel = ModelLoader.Load(modelAsset);
        // Initialize the worker for a CPU device (you can change this to GPU if needed)
        worker = new Worker(runtimeModel, Unity.Sentis.DeviceType.CPU);
    }

    public void ClassifyNPC(string npcDescription)
    {
        // Start a coroutine to handle the asynchronous execution of the model
        StartCoroutine(ExecuteModel(npcDescription));
    }

    private IEnumerator ExecuteModel(string npcDescription)
    {
        // Preprocess the input text and convert it to a tensor
        Tensor<float> inputTensor = Preprocess(npcDescription);

        // Execute the model by scheduling the tensor for processing
        worker.Schedule(inputTensor);

        // Wait for the model execution to complete
        while (!worker.PeekOutput().IsReadbackRequestDone())
        {
            yield return null;
        }

        // Get the output tensor
        Tensor output = worker.PeekOutput();

        // Postprocess the output to get classification
        string personality = Postprocess(output);

        Debug.Log("NPC Personality: " + personality);

        // Dispose of the tensors after use
        inputTensor.Dispose();
        output.Dispose();
    }

    Tensor<float> Preprocess(string text)
    {
        // Implement text tokenization and conversion to tensor
        // This is a placeholder for demonstration purposes
        TensorShape shape = new TensorShape(1, 384);
        float[] data = new float[shape.length];
        // Populate 'data' with actual preprocessed values (text encoding, etc.)
        return new Tensor<float>(shape, data);
    }

    string Postprocess(Tensor output)
    {
        // Implement logic to interpret the model's output
        // This is a placeholder for demonstration purposes
        return "Adventurous"; // Example personality trait
    }

    void OnDestroy()
    {
        // Dispose of the worker when done
        worker.Dispose();
    }
}
