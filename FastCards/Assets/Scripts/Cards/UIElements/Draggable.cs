using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Card card;
    private RectTransform rectTransform;
    private Vector2 startingPosition;

    private int unitsToShowCard = (int)(Screen.height * 0.23f);

    private int siblingIndex;

    private bool cardUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
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
