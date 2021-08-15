using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Save_Team saveTeam;
    public GameObject[] listChars;
    public void Start()
    {
        saveTeam = GameObject.Find("SaveTeam").GetComponent<Save_Team>();
        ShowTeam();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowTeam()
    {
        for(int i = 0; i < saveTeam.characters.Length; i++)
        {
            if(saveTeam.characters[i] == 1)
                listChars[i].SetActive(true);
        }
    } 
}
