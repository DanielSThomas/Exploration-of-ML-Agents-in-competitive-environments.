using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class EliminationGameManager : MonoBehaviour
{


    [SerializeField] private int maxSteps = 10000;
    int matchtimer;

    [SerializeField] private List<EliminationAgent> allAgents = new List<EliminationAgent>();

    [SerializeField] private SimpleMultiAgentGroup redTeamAgents;
    [SerializeField] private SimpleMultiAgentGroup blueTeamAgents;

    [SerializeField] private int scoreTarget;
    
    private int redTeamScore;
    private int blueTeamScore;


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

    void FixedUpdate()
    {
        //Enviroment Timer
        matchtimer += 1;
        if(matchtimer > maxSteps)
        {
            redTeamAgents.GroupEpisodeInterrupted();
            blueTeamAgents.GroupEpisodeInterrupted();
            RoundStart();
        }

        if (redTeamScore == scoreTarget)
        {
            
            RoundOver(redTeamAgents,blueTeamAgents);
        }

        if (blueTeamScore == scoreTarget)
        {
            
            RoundOver(blueTeamAgents,redTeamAgents);
        }


    }

    private void RoundStart()
    {
        matchtimer = 0;
        redTeamScore = 0;
        blueTeamScore = 0;

    }


    private void RoundOver(SimpleMultiAgentGroup winningTeam, SimpleMultiAgentGroup losingTeam)
    {

        Debug.Log(winningTeam + " Won");
        winningTeam.AddGroupReward(1 - matchtimer / maxSteps);
        losingTeam.AddGroupReward(-1);

        redTeamAgents.EndGroupEpisode();
        blueTeamAgents.EndGroupEpisode();

        RoundStart();
    }


    public int getRedScore()
    {
        return redTeamScore;
    }

    public void setRedScore(int value)
    {
        redTeamScore = value;
    }

    public int getBlueScore()
    {
        return redTeamScore;
    }

    public void setBlueScore(int value)
    {
        redTeamScore = value;
    }



}
