// Creates a simple wizard that lets you create a Light GameObject
// or if the user clicks in "Apply", it will set the color of the currently
// object selected to red

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class CreateCardTool : ScriptableWizard
{
    public string cardName;
    [TextArea]
    public string cardDescription;
    public int cost;
    public bool discard;

    public Card.CardType type;

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
        //StringBuilder sb = new StringBuilder();
        //StringWriter sw = new StringWriter();

        //using (JsonWriter writer = new JsonTextWriter(sw))
        //{
        //    writer.Formatting = Formatting.Indented;
        //    writer.AutoCompleteOnClose = true;

        //    //Staring JSON object
        //    writer.WriteStartObject();
        //    writer.WritePropertyName("Card Name");
        //    writer.WriteValue(cardName);

        //    writer.WritePropertyName("Card Description");
        //    writer.WriteValue(cardDescription);

        //    writer.WritePropertyName("Card Type");
        //    writer.WriteValue(type);

        //    writer.WritePropertyName("Card Cost");
        //    writer.WriteValue(cost);

        //    writer.WritePropertyName("Card Discard");
        //    writer.WriteValue(discard);

        //    //writer.WritePropertyName("Card Behaviours");
        //    //writer.WriteStartObject();

        //    //for (int i = 0; i < cardBehaviour.Length; i++)
        //    //{
        //    //    writer.WritePropertyName("Behaviour Conditions");
        //    //    writer.WriteStartObject();
        //    //    for (int j = 0; j < cardBehaviour[i].condition.deleagtePairs.Length; j++)
        //    //    {
        //    //        writer.WritePropertyName("First Delegate");
        //    //        writer.WriteValue(cardBehaviour[i].condition.deleagtePairs[j].delegate1);

        //    //        writer.WritePropertyName("Second Delegate");
        //    //        writer.WriteValue(cardBehaviour[i].condition.deleagtePairs[j].delegate2);

        //    //        writer.WritePropertyName("Condition Type");
        //    //        writer.WriteValue(cardBehaviour[i].condition.conditionTypes[i]);
        //    //    }
        //    //    writer.WriteEnd();
        //    //    writer.WriteEndObject();

        //    //    writer.WritePropertyName("Behaviour Value");
        //    //    writer.WriteValue(cardBehaviour[i].value);

        //    //    writer.WritePropertyName("Behaviour Target");
        //    //    writer.WriteValue(cardBehaviour[i].targetPlayer);

        //    //    writer.WritePropertyName("Behaviour Type");
        //    //    writer.WriteValue(cardBehaviour[i].behaviourType);

        //    //}

        //    //writer.WriteEnd();
        //    //writer.WriteEndObject();
        //    writer.WriteEndObject();
        //    writer.WriteEnd();
        //}

        //Debug.Log(sb.ToString());

        Card card = new Card() {
            cardName = cardName,
            cardDescription = cardDescription,
            cardType = type,
            cost = cost,
            discard = discard,
        };

        for (int i = 0; i < cardBehaviour.Length; i++)
        {
            card.cardBehaviours.Add(cardBehaviour[i]);

            switch (card.cardBehaviours[i].behaviourType)
            {
                case CardBehaviour.BehaviourType.Attack:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.AttackCard;
                    break;
                case CardBehaviour.BehaviourType.Defense:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.DefenseCard;
                    break;
                case CardBehaviour.BehaviourType.Heal:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.HealCard;
                    break;
                case CardBehaviour.BehaviourType.TakeHp:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.TakeHPCard;
                    break;
                case CardBehaviour.BehaviourType.BuffAttack:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.StatusAttack;
                    break;
                case CardBehaviour.BehaviourType.BuffDefense:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.StatusDefense;
                    break;
                case CardBehaviour.BehaviourType.BuffHealing:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.StatusHeal;
                    break;
                case CardBehaviour.BehaviourType.Draw:
                    card.cardBehaviours[i].behaviorUse += CardBehaviour.DrawCard;
                    break;
            }
        }

        //List<Card> cardList = JsonConvert.DeserializeObject<List<Card>>(jsonData);
        //cardList.Add(card);

        //jsonData = JsonConvert.SerializeObject(cardList);
        //Debug.Log(jsonData);
        string jsonData = File.ReadAllText(filePath);
        string cardData = JsonUtility.ToJson(card);
        //jsonData += ",\n" + cardData
        cardData = cardData.Substring(0, cardData.Length - 1);

        int ind = jsonData.LastIndexOf("}");
        jsonData = jsonData.Insert(ind, "},\n" + cardData);
        File.WriteAllText(filePath, jsonData);


        //string output = JsonConvert.SerializeObject(card);
        //Debug.Log("Output: " + output);

        //GameManager.cardsCreated.Add(JsonConvert.DeserializeObject(output) as Card);
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