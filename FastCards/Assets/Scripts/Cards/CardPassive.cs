using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Passive Card", menuName = "Card/Passive Card")]
public class CardPassive : Card
{
    public AllPasives.PassiveName selectPassive;

    public void PassiveCard(Card card)
    {
        AllPasives.passives[selectPassive].Invoke(card);
    }

    public override void CardInit()
    {
        cardType = CardType.Passive;
    }

    public override void CardUse()
    {
        CardPassive cardPassive = this;
        Deck.passives.Add(cardPassive);
    }
}
