using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool selected;
    GameMaster gm;
    public int tileSpeed;
    public bool hasMoved;
    public float moveSpeed;

    public int playerNumber;

    public int attackRange;
    List<Unit> enemiesInRange = new List<Unit>();
    public bool hasAttacked;

    public GameObject targetIcon;

    public Unit closestEnemy;
    public float closest;

    public int health;
    public int attackDamage;
    public int defenseDamage;
    public int armor;

    public Damage damageIcon;
    public GameObject deathEffect;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        ResetTargetIcons();

        if (selected == true)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        }
        else
        {
            if (playerNumber == gm.playerTurn)
            {


                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }
                selected = true;
                gm.selectedUnit = this;
                gm.ToggleStatsPanel(this);
                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>();
        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.hasAttacked == false)
            {
                gm.selectedUnit.Attack(unit);
            }
        }

    }

    public void Attack(Unit enemy)
    {
        hasAttacked = true;
        int enemyDamage = attackDamage - enemy.armor;
        int myDamage = enemy.defenseDamage - armor;

        if (enemyDamage >= 1)
        {
            Damage instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
            instance.Setup(enemyDamage);
            enemy.health -= enemyDamage;
        }

        if (myDamage >= 1)
        {
            Damage instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
            instance.Setup(myDamage);
            health -= myDamage;
        }

        if (enemy.health <= 0)
        {
            Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
            Destroy(enemy.gameObject);
            GetWalkableTiles();
            gm.RemoveStatusPanel(enemy);
            if (gm.playerTurn == 1)
                gm.teamTwo -= 1;
            else
                gm.teamOne -= 1;
            gm.CheckWin();
        }
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gm.ResetTiles();
            gm.RemoveStatusPanel(this);
            Destroy(gameObject);
            if (gm.playerTurn == 1)
                gm.teamOne -= 1;
            else
                gm.teamTwo -= 1;
            gm.CheckWin();

        }
        gm.UpdateStatsPanel();
    }

    public void GetWalkableTiles()
    {
        if (hasMoved == true)
        {
            return;
        }
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            {
                if (tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }

    public void GetEnemies()
    {
        closest = 1000;
        enemiesInRange.Clear();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            float distance = Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y);
            if (distance <= closest && unit.playerNumber != gm.playerTurn)
            {
                closest = distance;
                closestEnemy = unit;
            }
                
            if (distance <= attackRange)
            {
                if (unit.playerNumber != gm.playerTurn && hasAttacked == false)
                {
                    enemiesInRange.Add(unit);
                    unit.targetIcon.SetActive(true);
                }
            }
        }
    }

    public void ResetTargetIcons()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.targetIcon.SetActive(false);
        }
    }
    public void Move(Vector3 tilePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePos));
    }
    IEnumerator StartMovement(Vector3 tilePos)
    {
        while (transform.position.x != tilePos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(tilePos.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            yield return null;

        }
        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, tilePos.y, transform.position.z), moveSpeed * Time.deltaTime);
            yield return null;

        }
        hasMoved = true;
        ResetTargetIcons();
        GetEnemies();
    }
    public void ChoseTileEnemy()
    {
        float distanceClosest = Mathf.Abs(transform.position.x - closestEnemy.transform.position.x) + Mathf.Abs(transform.position.y - closestEnemy.transform.position.y);
        if (distanceClosest <= attackRange)
        {
            return;
        }
        Tile choosed = null;
        float maxdist = 1000;
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed &&
                Mathf.Abs(closestEnemy.transform.position.x - tile.transform.position.x) + Mathf.Abs(closestEnemy.transform.position.y - tile.transform.position.y) <= distanceClosest)
            {
                if (tile.IsClear() == true &&
                    Mathf.Abs(closestEnemy.transform.position.x - tile.transform.position.x) + Mathf.Abs(closestEnemy.transform.position.y - tile.transform.position.y) <= maxdist)
                {
                    choosed = tile;
                    maxdist = Mathf.Abs(closestEnemy.transform.position.x - tile.transform.position.x) + Mathf.Abs(closestEnemy.transform.position.y - tile.transform.position.y);
                }
            }
        }
        if(choosed!=null)
            Move(choosed.transform.position);
    }
}
