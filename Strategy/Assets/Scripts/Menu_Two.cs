using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Two : MonoBehaviour
{
    public GameObject menuTwo;
    public GameObject teamPick;
    public Save_Team saveTeam;
    public GameObject fullTeam;
    private int teamMax;

    private void Start()
    {
        saveTeam = GameObject.Find("SaveTeam").GetComponent<Save_Team>();
        teamMax = CheckTeamNumbers();
    }
    private int CheckTeamNumbers()
    {
        int j = 0;
        for(int  i = 0; i < saveTeam.characters.Length; i++)
        {
            if (saveTeam.characters[i] == 1)
                j++;
        }

        return j;
    }
    public void PlayCemitery()
    {
        SceneManager.LoadScene("Cemitery");
    }
    public void BackMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PickTeam()
    {
        menuTwo.SetActive(false);
        teamPick.SetActive(true);
    }
    public void BacKmenuTwo()
    {
        teamPick.SetActive(false);
        menuTwo.SetActive(true);
    }
    public void AddMinotaur()
    {
        if (teamMax < 4)
        {
            saveTeam.characters[4] = 1;
            teamMax++;
        }
        else
            fullTeam.SetActive(true);
    }
    public void AddTrog()
    {
        if (teamMax < 4)
        {
            saveTeam.characters[5] = 1;
            teamMax++;
        }
        else
            fullTeam.SetActive(true);
    }
}
