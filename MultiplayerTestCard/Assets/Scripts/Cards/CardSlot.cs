using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public Card card;

    private void OnEnable()
    {
        if(GetComponent<DropableArea>() != null)
        {
            GetComponent<DropableArea>().onDropedCard += handleDropedCard;
        }
    }

    public Card getCard()
    {
        return this.card;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleDropedCard(Card card, PointerEventData pointerData)
    {
        if(GetComponent<HorizontalCardHolder>() != card.currentHolder)
        {
            card.currentHolder.sendToOtherHolder(GetComponent<HorizontalCardHolder>(), card);
        }
    }
}
