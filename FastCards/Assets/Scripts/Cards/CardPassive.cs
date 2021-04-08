using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardPassive : Card
{
    public AllPasives.PassiveName selectPassive;

    public void PassiveCard()
    {
        AllPasives.passives[selectPassive].Invoke();
        //card.passivesApplied.Add(this);
    }

    public override void CardInit()
    {
        cardType = CardType.Passive;
    }

    public override void CardUse()
    {
        //CardPassive cardPassive = this;
        //Deck.passives.Add(cardPassive);
        //GameManager.combatManager.passivesInPlay.Add(this);
    }
}