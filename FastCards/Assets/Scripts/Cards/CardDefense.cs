using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Defense Card", menuName = "Card/Defense Card")]
public class CardDefense : Card
{
    public enum DefenseType
    {
        DefenseOnly,
        DefenseWAttack,
        DefenseWStatus,
        DefenseWHeal
    }

    public DefenseType defenseType;

    public void DefenseCard(PlayerFunctions player)
    {
        player.AddArmor(armor);
    }

    //Combined Defenses
    public void DefenseWithAttack(PlayerFunctions player)
    {
        player.AddArmor(armor);
        int fullDamage = damage + player.GetPlayer().GetStatusDamage();
        if (fullDamage >= 0)
            player.TakeDamage(fullDamage);
    }

    public void DefenseWithHealing(PlayerFunctions player)
    {
        player.AddArmor(armor);
        player.Heal(heal);
    }

    public void DefenseWithStatus(PlayerFunctions player)
    {
        player.AddArmor(armor);
        player.ApplyStatus(statusDamage, statusDefense, statusHealing);
    }

    public override void CardInit()
    {
        cardType = CardType.Defense;

        switch (defenseType)
        {
            case DefenseType.DefenseOnly:
                cardUse += DefenseCard;
                break;
            case DefenseType.DefenseWAttack:
                cardUse += DefenseWithAttack;
                break;
            case DefenseType.DefenseWHeal:
                cardUse += DefenseWithHealing;
                break;
            case DefenseType.DefenseWStatus:
                cardUse += DefenseWithStatus;
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

[CustomEditor(typeof(CardDefense))]
class DefenseInspectorEditor : Editor
{

    //cast target
    CardDefense enumScript;

    public override void OnInspectorGUI()
    {
        enumScript = target as CardDefense;

        //Enum drop down
        enumScript.cardName = EditorGUILayout.TextField("Name", enumScript.cardName);
        enumScript.cardDescription = EditorGUILayout.TextField("Description", enumScript.cardDescription);
        enumScript.cost = EditorGUILayout.IntField("Cost", enumScript.cost);
        enumScript.discard = EditorGUILayout.Toggle("Discard", enumScript.discard);
        enumScript.self = EditorGUILayout.Toggle("Self", enumScript.self);
        enumScript.armor = EditorGUILayout.IntField("Defense", enumScript.armor);
        enumScript.defenseType = (CardDefense.DefenseType)EditorGUILayout.EnumPopup(enumScript.defenseType);

        //switch statement for different variables

        switch (enumScript.defenseType)
        {
            case CardDefense.DefenseType.DefenseOnly:
                break;
            case CardDefense.DefenseType.DefenseWAttack:
                enumScript.damage = EditorGUILayout.IntField("Damage", enumScript.damage);
                break;
            case CardDefense.DefenseType.DefenseWHeal:
                enumScript.heal = EditorGUILayout.IntField("Heal", enumScript.heal);
                break;
            case CardDefense.DefenseType.DefenseWStatus:
                enumScript.statusDamage = EditorGUILayout.IntField("Status Damage", enumScript.statusDamage);
                enumScript.statusDefense = EditorGUILayout.IntField("Status Defense", enumScript.statusDefense);
                enumScript.statusHealing = EditorGUILayout.IntField("Status Healing", enumScript.statusHealing);
                break;
        }
    }
}//end inspectorclass

#endif