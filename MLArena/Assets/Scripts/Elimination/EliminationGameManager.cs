using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class EliminationGameManager : MonoBehaviour
{


    [SerializeField] private int maxSteps = 10000;

    [SerializeField] private List<EliminationAgent> allAgents = new List<EliminationAgent>();

    [SerializeField] private SimpleMultiAgentGroup redTeamAgents;
    [SerializeField] private SimpleMultiAgentGroup blueTeamAgents;
    

    // Start is called before the first frame update
    void Start()
    {
        redTeamAgents = new SimpleMultiAgentGroup();
        blueTeamAgents = new SimpleMultiAgentGroup();


        for (int i = 0; i < allAgents.Count; i++)
        {
            if(allAgents[i].getTeam() == 0)
            {
                redTeamAgents.RegisterAgent(allAgents[i]);
            }
            else
            {
                blueTeamAgents.RegisterAgent(allAgents[i]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
