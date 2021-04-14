using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class CardBehaviour
{
    [SerializeField] public Condition condition = new Condition();

    public int value = 0;
    int sumtotal = 0;

    int firstDebuff = 50 + (GameManager.currentEnemy * 2);
    int secondDebuff = 100 + (GameManager.currentEnemy * 2);
    int thirdDebuff = 200 + (GameManager.currentEnemy * 2);
    int fourthDebuff = 300 + (GameManager.currentEnemy * 2);

    public bool targetPlayer = true;
    [SerializeField]public LookUpTable.DelegateType valueDelegate;
    [SerializeField] public AllPasives.PassiveName selectPassive;

    [System.Serializable] public enum BehaviourType
    {
        Attack,
        Defense,
        Heal,
        TakeHp,
        BuffAttack,
        BuffDefense,
        BuffHealing,
        Draw,
        Passive
    }

    [SerializeField] public BehaviourType behaviourType;

    [SerializeField] public System.Action<bool, int, LookUpTable.DelegateType> behaviorUse;
    [SerializeField] public System.Action passiveBehaviour;

    public void Init()
    {
        switch (behaviourType)
        {
            case BehaviourType.Attack:
                behaviorUse += AttackCard;
                break;
            case BehaviourType.Defense:
                behaviorUse += DefenseCard;
                break;
            case BehaviourType.Heal:
                behaviorUse += HealCard;
                break;
            case BehaviourType.TakeHp:
                behaviorUse += TakeHPCard;
                break;
            case BehaviourType.BuffAttack:
                behaviorUse += StatusAttack;
                break;
            case BehaviourType.BuffDefense:
                behaviorUse += StatusDefense;
                break;
            case BehaviourType.BuffHealing:
                behaviorUse += StatusHeal;
                break;
            case BehaviourType.Draw:
                behaviorUse += DrawCard;
                break;
            case BehaviourType.Passive:
                passiveBehaviour += PassiveCard;
                break;
        }
    }

    public int CheckDamageBehaviour()
    {
        
        switch (behaviourType)
        {
            case BehaviourType.Attack:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]() + GameManager.player.GetPlayer().GetStatusDamage() + GameManager.combatManager.combo;
                break;
            case BehaviourType.Defense:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]() + GameManager.player.GetPlayer().GetStatusDefense() + GameManager.combatManager.combo;

                if (GameManager.player.GetPlayer().GetCurrentArmor() >= firstDebuff && GameManager.player.GetPlayer().GetCurrentArmor() < secondDebuff)
                {
                    sumtotal = Mathf.RoundToInt(sumtotal * 0.75f);
                } 
                else if (GameManager.player.GetPlayer().GetCurrentArmor() >= secondDebuff && GameManager.player.GetPlayer().GetCurrentArmor() < thirdDebuff)
                {
                    sumtotal = Mathf.RoundToInt(sumtotal * 0.5f);
                }
                else if (GameManager.player.GetPlayer().GetCurrentArmor() >= thirdDebuff && GameManager.player.GetPlayer().GetCurrentArmor() < fourthDebuff)
                {
                    sumtotal = Mathf.RoundToInt(sumtotal * 0.25f);
                }
                else if (GameManager.player.GetPlayer().GetCurrentArmor() >= fourthDebuff)
                {
                    sumtotal = Mathf.RoundToInt(sumtotal * 0.1f);
                }

                break;
            case BehaviourType.Heal:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]() + GameManager.player.GetPlayer().GetStatusHeal() + GameManager.combatManager.combo;
                break;
            case BehaviourType.TakeHp:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]();
                break;
            case BehaviourType.BuffAttack:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]();
                break;
            case BehaviourType.BuffDefense:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]();
                break;
            case BehaviourType.BuffHealing:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]();
                break;
            case BehaviourType.Draw:
                sumtotal = value + LookUpTable.lookUpTable[valueDelegate]();
                break;
        }
        return sumtotal;
    }
    public void Execute()
    {
        if (behaviourType != BehaviourType.Passive)
        {
            if (condition.CheckCondition())
                behaviorUse.Invoke(targetPlayer, value, valueDelegate);

            switch (behaviourType)
            {
                case BehaviourType.Attack:
                    CombatManager.attackCardsRound++;
                    break;
                case BehaviourType.Defense:
                    CombatManager.defendCardsRound++;
                    break;
                case BehaviourType.Heal:
                    CombatManager.healingCardsRound++;
                    break;
                case BehaviourType.TakeHp:
                    break;
                case BehaviourType.BuffAttack:
                    CombatManager.statusCardsRound++;
                    break;
                case BehaviourType.BuffDefense:
                    CombatManager.statusCardsRound++;
                    break;
                case BehaviourType.BuffHealing:
                    CombatManager.statusCardsRound++;
                    break;
                case BehaviourType.Draw:
                    CombatManager.cardsDrawn++;
                    break;
            }
        } else if (behaviourType == BehaviourType.Passive)
        {
            if (condition.CheckCondition())
                passiveBehaviour.Invoke();
        }
        
    }

    public void AttackCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int fullDamage = value + LookUpTable.lookUpTable[del]() + GameManager.player.GetPlayer().GetStatusDamage() + GameManager.combatManager.combo;
        if (self)
        {
            GameManager.player.TakeDamage(fullDamage);
        }
        else if (!self)
        {
            CombatManager.enemy.TakeDamage(fullDamage);
            CombatManager.damageDealtRound += fullDamage;
            CombatManager.damageDealt += fullDamage;
        }

        
    }

    public void TakeHPCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int totalValue = value + LookUpTable.lookUpTable[del]();

        CombatManager.damageDealtRound += totalValue;
        CombatManager.damageDealt += totalValue;

        if (self)
            GameManager.player.LoseHP(totalValue);
        else if (!self)
            CombatManager.enemy.LoseHP(totalValue);

    }

    public void DefenseCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int totalValue = value + LookUpTable.lookUpTable[del]() + GameManager.combatManager.combo;

        if (GameManager.player.GetPlayer().GetCurrentArmor() >= firstDebuff && GameManager.player.GetPlayer().GetCurrentArmor() < secondDebuff)
        {
            totalValue = Mathf.RoundToInt(totalValue * 0.75f);
        }
        else if (GameManager.player.GetPlayer().GetCurrentArmor() >= secondDebuff && GameManager.player.GetPlayer().GetCurrentArmor() < thirdDebuff)
        {
            totalValue = Mathf.RoundToInt(totalValue * 0.5f);
        }
        else if (GameManager.player.GetPlayer().GetCurrentArmor() >= thirdDebuff && GameManager.player.GetPlayer().GetCurrentArmor() < fourthDebuff)
        {
            totalValue = Mathf.RoundToInt(totalValue * 0.25f);
        }
        else if (GameManager.player.GetPlayer().GetCurrentArmor() >= fourthDebuff)
        {
            totalValue = Mathf.RoundToInt(totalValue * 0.1f);
        }


        if (self)
        {
            GameManager.player.AddArmor(totalValue);
            CombatManager.armorDealtRound += totalValue;
            CombatManager.armorGotten += totalValue;
        }
        else if (!self)
        {
            CombatManager.enemy.AddArmor(totalValue);
        }

        
    }

    public void HealCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int totalValue = value + LookUpTable.lookUpTable[del]() + GameManager.combatManager.combo;

        if (self)
            GameManager.player.Heal(totalValue);
        else if (!self)
            CombatManager.enemy.Heal(totalValue);


        CombatManager.healingDealtRound += totalValue;
        CombatManager.healingDone += totalValue;
    }

    public void StatusAttack(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int totalValue = value + LookUpTable.lookUpTable[del]();

        if (self)
            GameManager.player.ApplyStatus(totalValue, 0, 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(totalValue, 0, 0);

        CombatManager.statusDealtRound += totalValue;
        CombatManager.statusInflicted += totalValue;
    }

    public void StatusDefense(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int totalValue = value + LookUpTable.lookUpTable[del]();

        if (self)
            GameManager.player.ApplyStatus(0, totalValue, 0);
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, totalValue, 0);

        CombatManager.statusDealtRound += totalValue;
        CombatManager.statusInflicted += totalValue;
    }

    public void StatusHeal(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int totalValue = value + LookUpTable.lookUpTable[del]();

        if (self)
            GameManager.player.ApplyStatus(0, 0, totalValue);
        else if (!self)
            CombatManager.enemy.ApplyStatus(0, 0, totalValue);

        CombatManager.statusDealtRound += totalValue;
        CombatManager.statusInflicted += totalValue;
    }

    public void DrawCard(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    {
        int totalValue = value + LookUpTable.lookUpTable[del]();

        for (int i = 0; i < value; i++)
        {
            Deck.instance.StartCoroutine(GameManager.deck.DrawCard(GameManager.player.GetPlayer()));
        }

        CombatManager.drawsDealtRound += totalValue;
        CombatManager.cardsDrawn += totalValue;
    }

    public void PassiveCard()
    {
        GameManager.combatManager.passivesInPlay.Add(selectPassive);
    }

    //public void RandomBuff(bool self, int value = 0, LookUpTable.DelegateType del = 0)
    //{
    //    int totalValue = value + LookUpTable.lookUpTable[del]();

    //    if (self)
    //        GameManager.player.ApplyStatus(0, 0, totalValue);
    //    else if (!self)
    //        CombatManager.enemy.ApplyStatus(0, 0, totalValue);

    //    CombatManager.statusDealtRound += totalValue;
    //    CombatManager.statusInflicted += totalValue;
    //}
}

[System.Serializable]
public class Condition
{
    public enum ConditionTypes
    {
        equal,
        lessThan,
        greaterThan,
        lessOrEqual,
        greaterOrEqual,
        different
    }

    [System.Serializable] public struct DeleagtePair
    {
        [SerializeField] public LookUpTable.DelegateType delegate1;
        [SerializeField] public LookUpTable.DelegateType delegate2;
    }

    [SerializeField] public DeleagtePair[] deleagtePairs;

    //[SerializeField] public Dictionary<LookUpTable.DelegateType, LookUpTable.DelegateType> conditionPairs;
    [SerializeField] public ConditionTypes[] conditionTypes;


    void Init()
    {
        
    }

    public bool CheckCondition()
    {
        int index = 0;
        bool result = true;

        foreach (var pair in deleagtePairs)
        {
            result &= CheckPairs(conditionTypes[index++], LookUpTable.lookUpTable[pair.delegate1](), LookUpTable.lookUpTable[pair.delegate2]());
        }
        return result;
    }

    public bool CheckPairs(ConditionTypes type, int a, int b)
    {
        switch (type)
        {
            case ConditionTypes.equal:
                return a == b;
            case ConditionTypes.lessThan:
                return a < b;
            case ConditionTypes.greaterThan:
                return a > b;
            case ConditionTypes.lessOrEqual:
                return a <= b;
            case ConditionTypes.greaterOrEqual:
                return a >= b;
            case ConditionTypes.different:
                return a != b;

        }
        return false;
    }
}


