using UnityEngine;
using UnityEngine.EventSystems;

//Component of cards when they're at player hand

[RequireComponent(typeof(RectTransform))]
public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Card card;
    private RectTransform rectTransform;
    private Vector2 startingPosition;

    //Height of card on screen
    private int unitsToShowCard = (int)(Screen.height * 0.23f);

    private int siblingIndex;

    private bool cardUsed = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        siblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        CombatManager.hand.enabled = false;
        startingPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + unitsToShowCard);
        rectTransform.localScale = new Vector2(1.5f, 1.5f);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetCardPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float playField = Screen.height * 0.2f;
        if ( playField <= rectTransform.anchoredPosition.y &&!cardUsed)
        {
            if(GameManager.player.GetPlayer().GetCurrentMana() >= card.cost &&
                GameManager.player.GetPlayer().GetCurrentMana() - card.cost >= 0)
            {
                cardUsed = true;
                GameManager.deck.UsedCardToPile(GameManager.player.GetPlayer(), card);
                GameManager.player.SpendMana(card);
                Destroy(gameObject);
                GameManager.deck.cardsGO.Remove(gameObject);
                CombatManager.hand.enabled = true;
                CombatManager.hand.spacing -= 100;
            } else
            {
                ResetCardPosition();
            }
           
        }
        else
        {
            ResetCardPosition();
        }
    }

    void ResetCardPosition()
    {
        rectTransform.anchoredPosition = startingPosition;
        transform.SetSiblingIndex(siblingIndex);
        CombatManager.hand.enabled = true;
        rectTransform.localScale = new Vector2(1.0f, 1.0f);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
