using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static PlayerFunctions player = new PlayerFunctions();

    public static Deck deck;

    void Awake()
    {
        player.Init();
        deck = GetComponent<Deck>();
    }

    // Update is called once per frame
   
}
