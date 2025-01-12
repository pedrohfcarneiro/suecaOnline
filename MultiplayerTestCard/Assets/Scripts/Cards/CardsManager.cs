using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CardNaipe
{
    Clubs,
    Hearts,
    Diamonds,
    Spades
}
public enum CardNumber
{
    AS,
    DOIS,
    TRES,
    QUATRO,
    CINCO,
    SEIS,
    SETE,
    OITO,
    NOVE,
    DEZ,
    VALETE,
    DAMA,
    REI
}

public class CardsManager : MonoBehaviour
{

    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private int cardsToSpawn = 1;
    public GameObject cardPrefab;
    public GameObject cardVisualPrefab;
    public CardPile drawPile;

    [Header("Cards Textures")]
    public List<Sprite> clubsTextures = new List<Sprite>();
    public List<Sprite> heartsTextures = new List<Sprite>();
    public List<Sprite> diamondsTextures = new List<Sprite>();
    public List<Sprite> spadesTextures = new List<Sprite>();

    public static Dictionary<CardNaipe, List<Sprite>> naipeTextureDict = new Dictionary<CardNaipe, List<Sprite>>();

    public List<Card> allCards = new List<Card>();

    private void Awake()
    {
        naipeTextureDict.Add(CardNaipe.Clubs, clubsTextures);
        naipeTextureDict.Add(CardNaipe.Hearts, heartsTextures);
        naipeTextureDict.Add(CardNaipe.Diamonds, diamondsTextures);
        naipeTextureDict.Add(CardNaipe.Spades, spadesTextures);
    }

    // Start is called before the first frame update
    void Start()
    {
        //spawnCardsTest();
        createDeckCards();
        initializeDrawPile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: use virtual cards
    public void spawnCardsTest()
    {
        for(int i = 0; i < cardsToSpawn; i++)
        {
            GameObject slot = GameObject.Instantiate(playerHand.GetComponent<HorizontalCardHolder>().SlotPrefab, playerHand.transform);
            int randNaipe = Random.Range(0, 3);
            int randNumb = Random.Range(0, 12);
            Card card = spawnCard((CardNaipe) randNaipe, (CardNumber) randNumb, true, slot);
            playerHand.GetCardHolder().addCard(card, slot);
        }
    }

    public Card spawnCard(CardNaipe cardNaipe, CardNumber cardNumberToEnum, bool isActive, GameObject parent)
    {
        GameObject cardGO = GameObject.Instantiate(cardPrefab, parent.transform);
        Card card = cardGO.GetComponent<Card>();
        card.naipe = cardNaipe;
        card.number = cardNumberToEnum;
        card.GetComponent<Selectable>().enabled = isActive;

        //spawn visual
        GameObject cardVisualGO = GameObject.Instantiate(cardVisualPrefab, card.GetComponentInParent<Canvas>().transform);
        cardVisualGO.transform.position = cardGO.transform.position;
        cardVisualGO.GetComponent<Image>().sprite = naipeTextureDict[cardNaipe][(int) cardNumberToEnum];
        cardVisualGO.GetComponent<Image>().enabled = isActive;
        CardVisual cardVisual = cardVisualGO.GetComponent<CardVisual>();
        cardVisual.Initialize(card, card.parentIndex);

        card.cardVisual = cardVisualGO;

        return card;
    }

    public void moveCardToHand(Card card)
    {
        GameObject slot = GameObject.Instantiate(playerHand.GetComponent<HorizontalCardHolder>().SlotPrefab, playerHand.transform);
        card.gameObject.transform.SetParent(slot.transform, false);
        playerHand.GetCardHolder().addCard(card, slot);
        card.GetComponent<Selectable>().enabled = true;
        card.cardVisual.GetComponent<Image>().enabled = true;
    }

    public void drawTest()
    {
        moveCardToHand(allCards[0]);
    }

    public void draw()
    {
        Card cardToDraw = drawPile.popCard();
        drawPile.sendCardTo(playerHand.cardPile, cardToDraw);
        moveCardToHand(cardToDraw);
    }

    private void initializeDrawPile()
    {
        drawPile.initialize(allCards);
    }

    private void createDeckCards()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                int naipe = i;
                int numb = j;
                Card card = spawnCard((CardNaipe)i, (CardNumber)j, false, drawPile.gameObject);
                allCards.Add(card);
            }
        }
    }
}
