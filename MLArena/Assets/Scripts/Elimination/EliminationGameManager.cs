using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class EliminationGameManager : MonoBehaviour
{


    [SerializeField] private int maxSteps = 10000;
    int matchtimer;

    [SerializeField] private int redTeamBotCount;
    [SerializeField] private int blueTeamBotCount;

    [SerializeField] private GameObject redTeamBotPrefab;
    [SerializeField] private GameObject blueTeamBotPrefab;

    [SerializeField] private GameObject[] levels;
    [SerializeField] private LevelInfo currentLevel;

    [SerializeField] private List<GameObject> allAgentObjects = new List<GameObject>();

    [SerializeField] private SimpleMultiAgentGroup redTeamAgents;
    [SerializeField] private SimpleMultiAgentGroup blueTeamAgents;

    [SerializeField] private int scoreTarget;
    
    private int redTeamScore;
    private int blueTeamScore;


    // Start is called before the first frame update
    void Start()
    {

        NewRandomLevel();

        SpawnAgents();



        //Register bots to teams
        redTeamAgents = new SimpleMultiAgentGroup();
        blueTeamAgents = new SimpleMultiAgentGroup();


        for (int i = 0; i < allAgentObjects.Count; i++)
        {
            if(allAgentObjects[i].GetComponent<EliminationAgent>().getTeam() == 0)
            {
                redTeamAgents.RegisterAgent(allAgentObjects[i].GetComponent<EliminationAgent>());
            }
            else
            {
                blueTeamAgents.RegisterAgent(allAgentObjects[i].GetComponent<EliminationAgent>());
            }
        }

    }

    void FixedUpdate()
    {
        //Enviroment Timer
        matchtimer += 1;
        if(matchtimer >= maxSteps)
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

    private void SpawnAgents()
    {

        List<int> occupiedSpawns = new List<int>();

        //Spawn in bots
        for (int i = 0; i < redTeamBotCount; i++)
        {
            int randomNo = Random.Range(0, currentLevel.getSpawnPoints().Length);

            bool retrySpawn = false;

            //Check for a valid spawn point
            for (int j = 0; j < occupiedSpawns.Count; j++)
            {
                if (randomNo == occupiedSpawns[j])
                {
                    retrySpawn = true;
                }
            }

            if(retrySpawn == false)
            {
                //On First Spawn Only//
                GameObject agentObject = Instantiate(redTeamBotPrefab);

                agentObject.GetComponent<EliminationAgent>().setSpawn(currentLevel.getSpawnPoints()[randomNo]);

                allAgentObjects.Add(agentObject);

                occupiedSpawns.Add(randomNo);
            }
            else if (retrySpawn == true)
            {
                i--;
            }
        }

        // ! So much for DRY code !
        for (int i = 0; i < blueTeamBotCount; i++)
        {
            int randomNo = Random.Range(0, currentLevel.getSpawnPoints().Length);

            bool retrySpawn = false;

            //Check for a valid spawn point
            for (int j = 0; j < occupiedSpawns.Count; j++)
            {
                if (randomNo == occupiedSpawns[j])
                {
                    retrySpawn = true;
                }
            }

            if (retrySpawn == false)
            {
                //On First Spawn Only//
                GameObject agentObject = Instantiate(blueTeamBotPrefab);

                agentObject.GetComponent<EliminationAgent>().setSpawn(currentLevel.getSpawnPoints()[randomNo]);

                allAgentObjects.Add(agentObject);

                occupiedSpawns.Add(randomNo);
            }
            else if (retrySpawn == true)
            {
                i--;
            }
        }


    }

    private void NewRandomLevel()
    {
        int randomNo = Random.Range(0, levels.Length);

        currentLevel = levels[randomNo].GetComponent<LevelInfo>();

        Instantiate(levels[randomNo]);

    }
   

    private void RoundStart()
    {
        matchtimer = 0;
        redTeamScore = 0;
        blueTeamScore = 0;
        //Revive all agents
        //Respawn
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
