using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Heal Card", menuName = "Card/Heal Card")]
public class CardHeal : Card
{
    public enum HealType
    {
        HealOnly,
        HealWAttack,
        HealWDefense,
        HealWStatus
    }

    public HealType healType;

    public void HealCard(PlayerFunctions player)
    {
        player.Heal(heal);
    }

    //Combined Healings
    public void HealingWithAttack(PlayerFunctions player)
    {
        player.Heal(heal);
        int fullDamage = damage + player.GetPlayer().GetStatusDamage();
        if (fullDamage >= 0)
            player.TakeDamage(fullDamage);
    }

    public void HealingWithDefense(PlayerFunctions player)
    {
        player.Heal(heal);
        player.AddArmor(armor);
    }

    public void HealingWithStatus(PlayerFunctions player)
    {
        player.Heal(heal);
        player.ApplyStatus(statusDamage, statusDefense, statusHealing);
    }

    public override void CardInit()
    {
        cardType = CardType.Heal;

        switch (healType)
        {
            case HealType.HealOnly:
                cardUse += HealCard;
                break;
            case HealType.HealWAttack:
                cardUse += HealingWithAttack;
                break;
            case HealType.HealWDefense:
                cardUse += HealingWithDefense;
                break;
            case HealType.HealWStatus:
                cardUse += HealingWithStatus;
                break;
        }
    }

    public override void CardUse()
    {
        cardUse?.Invoke(GameManager.player);
    }
}


//Custom inspector starts here
#if UNITY_EDITOR

[CustomEditor(typeof(CardHeal))]
class HealInspectorEditor : Editor
{

    //cast target
    CardHeal enumScript;

    public override void OnInspectorGUI()
    {
        enumScript = target as CardHeal;

        //Enum drop down
        enumScript.cardName = EditorGUILayout.TextField("Name", enumScript.cardName);
        enumScript.cardDescription = EditorGUILayout.TextField("Description", enumScript.cardDescription);
        enumScript.cost = EditorGUILayout.IntField("Cost", enumScript.cost);
        enumScript.discard = EditorGUILayout.Toggle("Discard", enumScript.discard);
        enumScript.self = EditorGUILayout.Toggle("Self", enumScript.self);
        enumScript.heal = EditorGUILayout.IntField("Heal", enumScript.heal);
        enumScript.healType = (CardHeal.HealType)EditorGUILayout.EnumPopup(enumScript.healType);

        //switch statement for different variables

        switch (enumScript.healType)
        {
            case CardHeal.HealType.HealOnly:
                break;
            case CardHeal.HealType.HealWAttack:
                enumScript.damage = EditorGUILayout.IntField("Damage", enumScript.damage);
                break;
            case CardHeal.HealType.HealWDefense:
                enumScript.armor = EditorGUILayout.IntField("Defense", enumScript.armor);
                break;
            case CardHeal.HealType.HealWStatus:
                enumScript.statusDamage = EditorGUILayout.IntField("Status Damage", enumScript.statusDamage);
                enumScript.statusDefense = EditorGUILayout.IntField("Status Defense", enumScript.statusDefense);
                enumScript.statusHealing = EditorGUILayout.IntField("Status Healing", enumScript.statusHealing);
                break;
        }
    }
}//end inspectorclass

#endif
