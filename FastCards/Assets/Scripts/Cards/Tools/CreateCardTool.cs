// Creates a simple wizard that lets you create a Light GameObject
// or if the user clicks in "Apply", it will set the color of the currently
// object selected to red

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
public class CreateCardTool : ScriptableWizard
{
    public string cardName;
    [TextArea]
    public string cardDescription;
    public int cost;
    public bool discard;

    public Card.CardType type;
    public Card.CardTier tier;

    public CardBehaviour[] cardBehaviour;

    [MenuItem("Tools/Create Card")]
    static void CreateWizard()
    {
        DisplayWizard<CreateCardTool>("Create Card", "Create");
        //If you don't want to use the secondary button simply leave it out:
        //ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create");
    }

    void OnWizardCreate()
    {
        string filePath = "Assets/Resources/Jsons/CardData.json";

        Card card = new Card() {
            cardName = cardName,
            cardDescription = cardDescription,
            cardType = type,
            cardTier = tier,
            cost = cost,
            discard = discard,
        };

        for (int i = 0; i < cardBehaviour.Length; i++)
        {
            card.cardBehaviours.Add(cardBehaviour[i]);

            switch (card.cardBehaviours[i].behaviourType)
            {
                case CardBehaviour.BehaviourType.Attack:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].AttackCard;
                    break;
                case CardBehaviour.BehaviourType.Defense:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].DefenseCard;
                    break;
                case CardBehaviour.BehaviourType.Heal:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].HealCard;
                    break;
                case CardBehaviour.BehaviourType.TakeHp:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].TakeHPCard;
                    break;
                case CardBehaviour.BehaviourType.BuffAttack:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].StatusAttack;
                    break;
                case CardBehaviour.BehaviourType.BuffDefense:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].StatusDefense;
                    break;
                case CardBehaviour.BehaviourType.BuffHealing:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].StatusHeal;
                    break;
                case CardBehaviour.BehaviourType.Draw:
                    card.cardBehaviours[i].behaviorUse += cardBehaviour[i].DrawCard;
                    break;
            }
        }

        string jsonData = File.ReadAllText(filePath);
        string cardData = JsonUtility.ToJson(card, true);
        cardData = cardData.Substring(0, cardData.Length - 1);

        int ind = jsonData.LastIndexOf("]");
        jsonData = jsonData.Insert(ind, ",\n" + cardData + "}\n");
        File.WriteAllText(filePath, jsonData);
    }

    void OnWizardUpdate()
    {
       // helpString = "Please set the color of the light!";
    }

    // When the user presses the "Apply" button OnWizardOtherButton is called.
    void OnWizardOtherButton()
    {
        if (Selection.activeTransform != null)
        {
            Light lt = Selection.activeTransform.GetComponent<Light>();

            if (lt != null)
            {
                lt.color = Color.red;
            }
        }
    }
}

#endif