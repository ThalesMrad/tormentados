using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] obstacleGraphics;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int randObs = Random.Range(0, obstacleGraphics.Length);
        rend.sprite = obstacleGraphics[randObs];
    }

   
}
