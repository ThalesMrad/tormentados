using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject tutorialMenu;
    public GameObject creditoMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene("MenuOne");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsClick()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void OptionsVoltar()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void TutorialClick()
    {
        mainMenu.SetActive(false);
        tutorialMenu.SetActive(true);
    }
    public void TutorialVoltar()
    {
        tutorialMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void CreditosClick()
    {
        mainMenu.SetActive(false);
        creditoMenu.SetActive(true);
    }
    public void CreditosVoltar()
    {
        creditoMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
