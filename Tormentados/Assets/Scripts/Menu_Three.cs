using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Three : MonoBehaviour
{
    public GameObject menuThree;
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
        for (int i = 0; i < saveTeam.characters.Length; i++)
        {
            if (saveTeam.characters[i] == 1)
                j++;
        }

        return j;
    }
    public void PlayDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }
    public void BackMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PickTeam()
    {
        menuThree.SetActive(false);
        teamPick.SetActive(true);
    }
    public void BacKmenuThree()
    {
        teamPick.SetActive(false);
        menuThree.SetActive(true);
    }
    public void AddGolem()
    {
        if (teamMax < 5)
        {
            saveTeam.characters[6] = 1;
            teamMax++;
        }
        else
            fullTeam.SetActive(true);
    }
    public void AddOsteon()
    {
        if (teamMax < 5)
        {
            saveTeam.characters[7] = 1;
            teamMax++;
        }
        else
            fullTeam.SetActive(true);
    }
}
