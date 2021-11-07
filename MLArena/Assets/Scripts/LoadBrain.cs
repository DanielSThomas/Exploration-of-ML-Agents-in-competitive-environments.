using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using Unity.Barracuda;

public class LoadBrain : MonoBehaviour
{
    [SerializeField] private BehaviorParameters bp;
    [SerializeField] private NNModel loadedBrain;


    // Start is called before the first frame update
    void Start()
    {
        bp = GetComponent<BehaviorParameters>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Loadbrain()
    {



        bp.Model = loadedBrain;
    }
}
