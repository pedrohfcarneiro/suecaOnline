using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private HorizontalCardHolder cardHolder = null;

    public CardPile cardPile;

    public HorizontalCardHolder GetCardHolder() { return cardHolder; }

    [SerializeField] HorizontalCardHolder slot1;

    [SerializeField] HorizontalCardHolder slot2;

    [SerializeField] HorizontalCardHolder slot3;

    public List<Card> getCards
    {
        get
        {
            return cardHolder.currentCards;
        }
        private set { }
    }

    private void Awake()
    {
        cardHolder = GetComponent<HorizontalCardHolder>();
        cardPile = GetComponent<CardPile>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
