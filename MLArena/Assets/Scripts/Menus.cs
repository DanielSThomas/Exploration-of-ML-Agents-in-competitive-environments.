using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [SerializeField] GameObject eliminationGameManager;

    [SerializeField] GameObject menugameobject;
    int redTeamBotCount = 1;
    int blueTeamBotCount = 1;
    bool humanPlayer = false;

    [SerializeField] Text blueTeamText;
    [SerializeField] Text redTeamText;
    [SerializeField] Slider blueSlider;
    [SerializeField] Slider redSlider;
    [SerializeField] Toggle humanPlayerToggle;


    // Start is called before the first frame update
    void Start()
    {
        




    }

    // Update is called once per frame
    void Update()
    {
        humanPlayer = humanPlayerToggle.isOn;
        blueTeamBotCount = (int)blueSlider.value;
        redTeamBotCount = (int)redSlider.value;

        blueTeamText.text = "Blue Team Count : " + blueTeamBotCount;
        redTeamText.text = "Red Team Count : " + redTeamBotCount;
    }

   

    public void ChangeHumanPlayer(bool value)
    {
        humanPlayer = value;
    }

    public void StartGame()
    {
        menugameobject.SetActive(false);

        Instantiate (eliminationGameManager);
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
