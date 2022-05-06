using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    EliminationGameManager eliminationGameManager;
    [SerializeField] GameObject eliminationGameManagerGameObject;

    [SerializeField] GameObject menugameobject;
    [SerializeField] GameObject scoremenugameobject;
    int redTeamBotCount = 1;
    int blueTeamBotCount = 1;
    bool humanPlayer = false;

    [SerializeField] Text blueTeamText;
    [SerializeField] Text redTeamText;
    [SerializeField] Slider blueSlider;
    [SerializeField] Slider redSlider;
    [SerializeField] Toggle humanPlayerToggle;

    [SerializeField] Text blueTeamWinsText;
    [SerializeField] Text redTeamWinsText;
    [SerializeField] Text EpisodeText;
    

    // Start is called before the first frame update
    void Start()
    {
        




    }

    // Update is called once per frame
    void Update()
    {
        if (menugameobject.activeInHierarchy == true)
        {
            humanPlayer = humanPlayerToggle.isOn;
            blueTeamBotCount = (int)blueSlider.value;
            redTeamBotCount = (int)redSlider.value;

            blueTeamText.text = "Blue Team Count : " + blueTeamBotCount;
            redTeamText.text = "Red Team Count : " + redTeamBotCount;
        }
        else if (scoremenugameobject.activeInHierarchy == true && eliminationGameManager !=null)
        {
            blueTeamWinsText.text = "Blue Team Wins : " + eliminationGameManager.getBlueWins().ToString();
            redTeamWinsText.text = "Red Team Wins : " + eliminationGameManager.getRedWins().ToString();

            EpisodeText.text = "Match : " + eliminationGameManager.getEpisode().ToString();
          
        }


       
    }

   

    public void ChangeHumanPlayer(bool value)
    {
        humanPlayer = value;
    }

    public void StartGame()
    {
        menugameobject.SetActive(false);

        scoremenugameobject.SetActive(true);

        GameObject eliminationGameManagerObject = Instantiate(eliminationGameManagerGameObject);

        eliminationGameManager = eliminationGameManagerObject.GetComponent<EliminationGameManager>();

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int GetRedTeamCount()
    {
        return redTeamBotCount;
    }

    public int GetBlueTeamCount()
    {
        return blueTeamBotCount;
    }

    public bool GetHumanPlayer()
    {
        return humanPlayer;
    }

}
