using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public int playerTurn = 1;
    public bool notIA = true;

    public Image turnIndicator;
    public Sprite player1Indicator;
    public Sprite player2Indicator;

    public GameObject statsPanel;
    public Unit viewedUnit;
    public Text healthText;
    public Text armorText;
    public Text attDamageText;
    public Text defDamageText;

    public Save_Team saveTeam;
    public GameObject[] chars;

    public int teamOne;
    public int teamTwo;
    public GameObject win;
    public GameObject defeat;

    public void ToggleStatsPanel(Unit unit)
    {
        if (unit.Equals(viewedUnit) == false)
        {
            statsPanel.SetActive(true);
            viewedUnit = unit;
            UpdateStatsPanel();
            
        }
        else
        {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
        
    }

    public void RemoveStatusPanel(Unit unit)
    {
        if (unit.Equals(viewedUnit))
        {
            statsPanel.SetActive(false);
        }
    }

    public void UpdateStatsPanel()
    {
        if(viewedUnit != null){
            healthText.text = viewedUnit.health.ToString();
            armorText.text = viewedUnit.armor.ToString();
            attDamageText.text = viewedUnit.attackDamage.ToString();
            defDamageText.text = viewedUnit.defenseDamage.ToString();
        }
    }
    public void ResetTiles()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }
    public void PlaceTeam()
    {
        int y = -1;
        for (int i = 0; i < saveTeam.characters.Length; i++)
        {
            if (saveTeam.characters[i] == 1)
            {
                Instantiate(chars[i], new Vector3(-4, y, 0), new Quaternion(0,0,0,0));
                y++;
            }
        }
    }
    void Start()
    {
        saveTeam = GameObject.Find("SaveTeam").GetComponent<Save_Team>();
        
        PlaceTeam();

        CountTeam();



    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    public void EndTurn()
    {
        if(playerTurn == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerTurn = 2;
            turnIndicator.sprite = player2Indicator;
            PlayIA();
        }else if (playerTurn == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            playerTurn = 1;
            turnIndicator.sprite = player1Indicator;
        }

        if(selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }
        ResetTiles();

        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.targetIcon.SetActive(false);
            unit.hasAttacked = false;
        }
    }
    public void CountTeam()
    {
        teamOne = 0;
        teamTwo = 0;
        foreach (Unit unit in FindObjectsOfType<Unit>()){
            if (unit.playerNumber == 1)
                teamOne++;
            else
                teamTwo++;
        }
    }

    public void CheckWin()
    {
        if(teamOne == 0)
        {
            TeamTwoWin();
        }
        else if(teamTwo == 0)
        {
            TeamOneWin();
        }
    }
    private void TeamOneWin()
    {
        win.SetActive(true);
    }
    private void TeamTwoWin()
    {
        defeat.SetActive(true);
    }

    public void GoMenuNext()
    {
        if (saveTeam.menu == 2)
        {
            saveTeam.menu++;
            SceneManager.LoadScene("MenuTwo");
        } else if(saveTeam.menu == 3){
            saveTeam.menu++;
            SceneManager.LoadScene("MenuThree");
        }
        else
        {
            SceneManager.LoadScene("Congratulations");
        }
            
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayIA()
    {
        
        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        
        StartCoroutine(MoveIa());
        
    }
    private IEnumerator MoveIa()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {

            if (unit.playerNumber == playerTurn)
            {

            
            selectedUnit = unit;
            unit.GetEnemies();
            unit.GetWalkableTiles();
            unit.ChoseTileEnemy();
            if (unit.closest <= unit.attackRange && unit.closestEnemy != null)
                unit.Attack(unit.closestEnemy);
            
            }
            yield return wait;
        }
        EndTurn();

    }
}
