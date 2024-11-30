using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalCardHolder : MonoBehaviour
{
    [SerializeField] private Card pickedCard = null;
    [SerializeField] private Card hoveredCard = null;

    public GameObject SlotPrefab;

    public List<GameObject> cardSlots = new List<GameObject>();

    public List<Card> currentCards = new List<Card>();

    public bool isCrossing = false;

    private void OnEnable()
    {
        Card.OnPointerEnterEvent += cardPointerEnter;
        Card.OnBeginDragEvent += beginDrag;
    }

    private void OnDisable()
    {
        Card.OnPointerEnterEvent -= cardPointerEnter;
        Card.OnBeginDragEvent -= beginDrag;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (pickedCard && !isCrossing)
        {
            for (int i = 0; i < currentCards.Count; i++)
            {
                if (pickedCard.transform.position.x > currentCards[i].transform.position.x)
                {
                    if (pickedCard.parentIndex < currentCards[i].parentIndex)
                    {
                        swap(i);
                        break;
                    }
                }

                if (pickedCard.transform.position.x < currentCards[i].transform.position.x)
                {
                    if (pickedCard.parentIndex > currentCards[i].parentIndex)
                    {
                        swap(i);
                        break;
                    }
                }
            }
        }
    }


    private void beginDrag(Card card, PointerEventData pointerData)
    {
        pickedCard = card;
    }

    void endDrag(Card card)
    {
        pickedCard = null;
    }

    void cardPointerEnter(Card card, PointerEventData pointerData)
    {
        hoveredCard = card;
    }

    void cardPointerExit(Card card, PointerEventData pointerData)
    {
        hoveredCard = null;
    }

    public void swap(int index)
    {
        isCrossing = true;

        Transform selectedParent = pickedCard.transform.parent;
        Transform crossedParent = currentCards[index].transform.parent;

        currentCards[index].transform.SetParent(selectedParent);
        currentCards[index].transform.localPosition = currentCards[index].isSelected ? new Vector3(0, currentCards[index].selectionOffset, 0) : Vector3.zero;
        pickedCard.transform.SetParent(crossedParent);

        

        //if (getCards[index].cardVisual == null)
        //{
        //    return;
        //}

        bool swapToRight = currentCards[index].parentIndex > pickedCard.parentIndex;

        int previousPickedParentIndex = pickedCard.parentIndex;
        pickedCard.parentIndex = currentCards[index].parentIndex;
        currentCards[index].parentIndex = previousPickedParentIndex;

        isCrossing = false;

        //getCards[index].cardVisual.swap(swapToRight ? -1 : 1);

        //foreach(Card card in getCards)
        //{
        //    card.cardVisual.updateIndex(transform.childCount);
        //}
    }

    //TODO: usar virtual cards (SO)
    public void addCard(Card card, GameObject slot)
    {
        cardSlots.Add(slot);
        card.currentSlot = slot;
        slot.GetComponent<CardSlot>().card = card;
        card.parentIndex = cardSlots.IndexOf(slot);
        currentCards.Add(card);
        card.currentHolder = this;
    }

    public Card removeCard(CardSlot slot)
    {
        Card cardToRemove = slot.GetComponent<CardSlot>().getCard();
        cardToRemove.currentSlot = null;
        cardToRemove.currentHolder = null;
        slot.GetComponent<CardSlot>().card = null;
        cardSlots.Remove(slot.gameObject);
        return cardToRemove;
    }

    public void sendToOtherHolder(HorizontalCardHolder holder, Card card)
    {
        removeCard(card.currentSlot.GetComponent<CardSlot>());
        holder.receiveCard(card);
    }

    public void receiveCard(Card card)
    {
        GameObject slot = GameObject.Instantiate(SlotPrefab, transform);
        card.gameObject.transform.SetParent(slot.transform, false);
        addCard(card, slot);
        card.GetComponent<Selectable>().enabled = true;
        card.cardVisual.GetComponent<Image>().enabled = true;
    }
}
