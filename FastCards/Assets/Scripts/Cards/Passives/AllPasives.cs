using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPasives 
{
    //Do a public static MAP with all de passives
    public enum PassiveName
    {
        Buff0Attacks,
    }

    public static Dictionary<PassiveName, System.Action<Card>> passives = new Dictionary<PassiveName, System.Action<Card>>();

    public static void Init()
    {
        passives.Add(PassiveName.Buff0Attacks, Buff0CostAttacks);
    }

    private static void Buff0CostAttacks(Card card)
    {
        if (card.cardType == Card.CardType.Attack)
        {
            if (card.cost == 0)
            {
                card.damage += 3;
            }
        }
    }

}
