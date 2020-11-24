using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static PlayerFunctions player = new PlayerFunctions();
    TextAsset jsonFile;

    public static Deck deck;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    player.Init();
    //    deck = GetComponent<Deck>();
    //    deck.Init();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        player.TakeDamage(6);
    //    }

    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        player.Heal(4);
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        deck.combatDeck[0].CardUse();
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        deck.combatDeck[1].CardUse();
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        deck.combatDeck[2].CardUse();
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        deck.combatDeck[3].CardUse();
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha5))
    //    {
    //        deck.combatDeck[4].CardUse();
    //    }


    //}
}
