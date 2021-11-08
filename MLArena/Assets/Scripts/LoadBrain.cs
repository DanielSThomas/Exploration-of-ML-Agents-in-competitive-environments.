using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;
using Unity.MLAgents.Policies;
using Unity.Barracuda;
using Unity.MLAgents;
using Unity.Barracuda.ONNX;

public class LoadBrain : MonoBehaviour
{

    

    private BehaviorParameters bp;

    private string path;

    private Agent agent;

    // Start is called before the first frame update
    void Start()
    {
        bp = GetComponent<BehaviorParameters>();
        agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //credit https://github.com/Unity-Technologies/ml-agents/blob/0.14.1/Project/Assets/ML-Agents/Examples/SharedAssets/Scripts/ModelOverrider.cs#L114-L128
    public void Loadbrain()
    {

        //string path = EditorUtility.OpenFilePanel("Load Brain", "", "onnx");


        byte[] model = null;

        try
        {
            model = File.ReadAllBytes(path);
        }
        catch (IOException)
        {
            Debug.Log($"Couldn't load file {path}", this);
                    
        }
        //Apparently loading a onnx file to unity in realtime is a pain... This code is mostly from Unity's own example from the link above.
        var converter = new ONNXModelConverter(true);
        var onnxModel = converter.Convert(model);

        NNModelData assetData = ScriptableObject.CreateInstance<NNModelData>();
        using (var memoryStream = new MemoryStream())
        using (var writer = new BinaryWriter(memoryStream))
        {
            ModelWriter.Save(writer, onnxModel);
            assetData.Value = memoryStream.ToArray();
        }
       
        var asset = ScriptableObject.CreateInstance<NNModel>();
        asset.modelData = assetData;
        asset.name = "Imported Brain";


        agent.SetModel(bp.BehaviorName, asset);


    }

    

}
