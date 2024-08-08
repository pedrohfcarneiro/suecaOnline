using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCardHolder : MonoBehaviour
{
    [SerializeField] private Card pickedCard = null;
    [SerializeField] private Card hoveredCard = null;

    public GameObject SlotPrefab;

    public List<GameObject> cardSlots = new List<GameObject>();

    public List<Card> currentCards = new List<Card>();

    public bool isCrossing = false;

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


    private void beginDrag(object sender, Card.CardInteractionEventArgs e)
    {
        pickedCard = e.card;
    }

    void endDrag(object sender, Card.CardInteractionEventArgs e)
    {
        pickedCard = null;
    }

    void cardPointerEnter(object sender, Card.CardInteractionEventArgs e)
    {
        hoveredCard = e.card;
    }

    void cardPointerExit(object sender, Card.CardInteractionEventArgs e)
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
        card.parentIndex = cardSlots.IndexOf(slot);
        card.OnPointerEnterEvent += cardPointerEnter;
        card.OnBeginDragEvent += beginDrag;

        currentCards.Add(card);
    }
}
