using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Team : MonoBehaviour
{
    public int[] characters;
    public int menu;
    private void Start()
    {
        menu = 2;
        DontDestroyOnLoad(this.gameObject);
        characters = new int[8] {0, 0, 0, 0, 0, 0, 0, 0};
    }
}
