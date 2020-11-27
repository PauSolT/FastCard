using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Passive Card", menuName = "Card/Passive Card")]
public class CardPassive : Card
{
    public AllPasives.PassiveName selectPassive;

    public void PassiveCard(Card card)
    {
        AllPasives.passives[selectPassive].Invoke(card);
        card.passivesApplied.Add(this);
    }

    public override void CardInit()
    {
        cardType = CardType.Passive;
    }

    public override void CardUse()
    {
        CardPassive cardPassive = this;
        Deck.passives.Add(cardPassive);
        base.CardUse();
    }
}

//Custom inspector starts here
#if UNITY_EDITOR

[CustomEditor(typeof(CardPassive))]
class PassiveInspectorEditor : Editor
{
    //cast target
    CardPassive enumScript;

    public override void OnInspectorGUI()
    {
        enumScript = target as CardPassive;

        //Enum drop down
        enumScript.cardName = EditorGUILayout.TextField("Name", enumScript.cardName);
        enumScript.cardDescription = EditorGUILayout.TextField("Description", enumScript.cardDescription);
        enumScript.cost = EditorGUILayout.IntField("Cost", enumScript.cost);
        enumScript.discard = EditorGUILayout.Toggle("Discard", enumScript.discard);
        enumScript.selectPassive = (AllPasives.PassiveName)EditorGUILayout.EnumPopup(enumScript.selectPassive);
        EditorUtility.SetDirty(target);
    }
}//end inspectorclass

#endif