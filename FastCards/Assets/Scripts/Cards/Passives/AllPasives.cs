using System.Collections.Generic;

public class AllPasives 
{
    //Do a public static MAP with all de passives
    public enum PassiveName
    {
        NoPassive,
        Buff0Attacks,
        Get1AttackPerRound,
    }

    public static Dictionary<PassiveName, System.Action> passives = new Dictionary<PassiveName, System.Action>();

    //Init MAP with passives
    public static void InitPassives()
    {
        //passives.Add(PassiveName.Buff0Attacks, Buff0CostAttacks);
        passives.Add(PassiveName.NoPassive, NoPassive);
        passives.Add(PassiveName.Get1AttackPerRound, Get1AttackPerRound);
    }

    private static void NoPassive()
    {

    }

    private static void Buff0CostAttacks(Card card)
    {
        if (card.cardType == Card.CardType.Attack)
        {
            if (card.cost == 0)
            {
                for (int i = 0; i < card.cardBehaviours.Count; i++)
                {
                    if(card.cardBehaviours[i].behaviourType == CardBehaviour.BehaviourType.Attack)
                    {
                        card.cardBehaviours[i].value += 3;
                    }
                }
            }
        }
    }

    private static void Get1AttackPerRound()
    {
        GameManager.player.ApplyStatus(1, 0, 0);
    }

}
