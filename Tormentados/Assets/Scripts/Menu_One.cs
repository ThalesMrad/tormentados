using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_One : MonoBehaviour
{
    public GameObject menuOne;
    public GameObject teamPick;
    public Save_Team saveTeam;
    public GameObject fullTeam;
    public GameObject warning;
    private int teamMax;

    private void Start()
    {
        saveTeam = GameObject.Find("SaveTeam").GetComponent<Save_Team>();
        teamMax = 0;
    }
    public void PlayWoods()
    {
        if(teamMax == 0)
        {
            warning.SetActive(true);
            return;
        }
        SceneManager.LoadScene("Woods");
    }
    public void BackMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PickTeam()
    {
        warning.SetActive(false);
        menuOne.SetActive(false);
        teamPick.SetActive(true);
    }
    public void BacKMenuOne()
    {
        teamPick.SetActive(false);
        menuOne.SetActive(true);
    }
    public void AddSamurai()
    {
        if (teamMax < 3 && saveTeam.characters[0] != 1)
        {
            saveTeam.characters[0] = 1;
            teamMax++;
        }
        else if (teamMax >= 3)
            fullTeam.SetActive(true);
    }
    public void AddArcher()
    {
        if(teamMax < 3 && saveTeam.characters[1] != 1)
        {
            saveTeam.characters[1] = 1;
            teamMax++;
        }       
        else if (teamMax >= 3)
            fullTeam.SetActive(true);
    }
    public void AddBard()
    {
        if (teamMax < 3 && saveTeam.characters[2] != 1)
        {
            saveTeam.characters[2] = 1;
            teamMax++;
        }
        else if(teamMax >= 3)
            fullTeam.SetActive(true);
    }
    public void AddMage()
    {
        if (teamMax < 3 && saveTeam.characters[3]!= 1)
        {
            saveTeam.characters[3] = 1;
            teamMax++;
        }
        else if(teamMax >=3)
            fullTeam.SetActive(true);
    }
}
