using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using UnityEngine.SceneManagement;
public class EliminationGameManager : MonoBehaviour
{
    [SerializeField] Menus menu;

    [SerializeField] private int maxSteps = 10001;
    [SerializeField] int matchtimer;

    [SerializeField] private int roundNumber = 0;

    [SerializeField] private int redTeamBotCount;
    [SerializeField] private int blueTeamBotCount;

    private bool humanPlayer;

    [SerializeField] private GameObject redTeamBotPrefab;
    [SerializeField] private GameObject blueTeamBotPrefab;

    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject currentLevel; 
    [SerializeField] private LevelInfo currentLevelInfo;

    [SerializeField] private List<GameObject> AgentObjects = new List<GameObject>();

   // [SerializeField] private List<GameObject> blueAgentObjects = new List<GameObject>();

    //private SimpleMultiAgentGroup redTeamAgents;
    //private SimpleMultiAgentGroup blueTeamAgents;


    [SerializeField] private int redTeamScore;

    [SerializeField] private int redTeamTotalWins;

    [SerializeField] private List<float> redTeamRewards;
 
   // [SerializeField] private float meanRedTeamReward;

    [SerializeField] private int blueTeamScore;

    [SerializeField] private int blueTeamTotalWins;

    [SerializeField] private List<float> blueTeamRewards;

    List<int> occupiedSpawns = new List<int>();

    int spawnIndex = 0;

    // [SerializeField] private float meanBlueTeamReward;

    private void Awake()
    {
        menu = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<Menus>();

        redTeamBotCount = menu.GetRedTeamCount();
        blueTeamBotCount = menu.GetBlueTeamCount();
        humanPlayer = menu.GetHumanPlayer();
    }


    // Start is called before the first frame update
    void Start()
    {
       

        NewRandomLevel();

        SpawnAgents(redTeamBotCount, redTeamBotPrefab);
        SpawnAgents(blueTeamBotCount, blueTeamBotPrefab);



        //Register bots to teams
        //redTeamAgents = new SimpleMultiAgentGroup();
        //blueTeamAgents = new SimpleMultiAgentGroup();

        //for (int i = 0; i < redAgentObjects.Count; i++)
        //{
        //    redTeamAgents.RegisterAgent(redAgentObjects[i].GetComponent<EliminationAgent>());
        //}
        //for (int i = 0; i < blueAgentObjects.Count; i++)
        //{
        //    blueTeamAgents.RegisterAgent(blueAgentObjects[i].GetComponent<EliminationAgent>());
        //}

    }

    private void Update()
    {
        //Restart Scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("EliminationTraining");
        }
    }

    void FixedUpdate()
    {

        //Enviroment Timer
        matchtimer += 1;
        if(matchtimer >= maxSteps)
        {
            Debug.Log("Time Limit Reached. Tie");

            //redTeamAgents.GroupEpisodeInterrupted();
            //blueTeamAgents.GroupEpisodeInterrupted();

            RoundOver();
        }

        if (redTeamScore == blueTeamBotCount)
        {
            Debug.Log("Red Team Won");
            redTeamTotalWins++;

            //redTeamRewards.Add(1 - (float)matchtimer / maxSteps);

            //float tempMath = 0;

            //for (int i = 0; i < redTeamRewards.Count; i++)
            //{
            //    tempMath += redTeamRewards[i];
            //}
           // meanRedTeamReward = tempMath / redTeamRewards.Count;


            RoundOver();
        }

        if (blueTeamScore == redTeamBotCount)
        {
            Debug.Log("Blue Team Won");
            blueTeamTotalWins++;

            //blueTeamRewards.Add(1 - (float)matchtimer / maxSteps);

            //float tempMath = 0;

            //for (int i = 0; i < blueTeamRewards.Count; i++)
            //{
            //    tempMath += blueTeamRewards[i];
            //}
            //meanBlueTeamReward = tempMath / blueTeamRewards.Count;


            RoundOver();
        }


    }

    private void SpawnAgents(int _agentcount, GameObject _agentobject)
    {

        

        //Spawn in bots
                 
        for (int i = 0; i < _agentcount; i++)
        {
            int randomNo = Random.Range(0, currentLevelInfo.getSpawnPoints().Length);

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
                GameObject agentObject;
                //On First Spawn Only// Test this
                if (roundNumber == 0)
                {
                    agentObject = Instantiate(_agentobject);

                    if (humanPlayer == true && i == 0 && spawnIndex == 0)
                    {
                        agentObject.GetComponent<Unity.MLAgents.Policies.BehaviorParameters>().BehaviorType = Unity.MLAgents.Policies.BehaviorType.HeuristicOnly;
                    }

                    AgentObjects.Add(agentObject);
                }
               
                //Reset spawn
                AgentObjects[spawnIndex].GetComponent<EliminationAgent>().setSpawn(currentLevelInfo.getSpawnPoints()[randomNo]);
                
                spawnIndex++;
                
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

        currentLevel = Instantiate(levels[randomNo]);

        currentLevelInfo = currentLevel.GetComponent<LevelInfo>();

    }

    private void RoundStart()
    {
        Destroy(currentLevel);

        matchtimer = 0;
        redTeamScore = 0;
        blueTeamScore = 0;
        roundNumber++;

        NewRandomLevel();
        SpawnAgents(redTeamBotCount,redTeamBotPrefab);
        SpawnAgents(blueTeamBotCount, blueTeamBotPrefab);
    }

    private void RoundOver()
    {

        //Delete leftover bullets
        GameObject[] _leftoverredbullets = GameObject.FindGameObjectsWithTag("RedBullet");      
        GameObject[] _leftoverbluebullets = GameObject.FindGameObjectsWithTag("BlueBullet");

        for (int i = 0; i < _leftoverredbullets.Length; i++)
        {
            GameObject.Destroy(_leftoverredbullets[i]);
        }
        for (int i = 0; i < _leftoverbluebullets.Length; i++)
        {
            GameObject.Destroy(_leftoverbluebullets[i]);
        }


        //EliminationAgent[] activeAgents = FindObjectsOfType<EliminationAgent>();

       // End all episodes
        for (int i = 0; i < AgentObjects.Count; i++)
        {

            AgentObjects[i].GetComponent<EliminationAgent>().endEpisode();
 
        }

        occupiedSpawns = new List<int>();

        spawnIndex = 0;

        RoundStart();

    }

    public int getRedWins()
    {
        return redTeamTotalWins;
    }

    public int getBlueWins()
    {
        return blueTeamTotalWins;
    }

    public float getRedAvg()
    {
        return AgentObjects[0].GetComponent<EliminationAgent>().CompletedEpisodes;
    }
    public float getBlueAvg()
    {
        return AgentObjects[1].GetComponent<EliminationAgent>().CompletedEpisodes;
    
    }




    public int getRedScore()
    {
        return redTeamScore;
    }

    public void addRedScore()
    {
        redTeamScore++;
    }

    public int getBlueScore()
    {
        return blueTeamScore;
    }

    public void addBlueScore()
    {
        blueTeamScore ++;
    }

    public int getMaxStep()
    {
        return maxSteps;
    }

}
