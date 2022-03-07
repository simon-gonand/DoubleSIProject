using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckBuilderManager : MonoBehaviour
{
    #region Variable
    public static DeckBuilderManager instance;

    private int playerID = 1;
    [SerializeField] private GameObject cardSelectParent;
    [SerializeField] private CardSelector[] cardSelectors /*= new List<CardSelector>()*/;

    private int cardCount = 0;
    private List<CardPreset> deck = new List<CardPreset>();

    [SerializeField] private Text cardCountText;
    [SerializeField] private Text playerIDText;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        cardSelectors = cardSelectParent.GetComponentsInChildren<CardSelector>();
        playerIDText.text = playerID.ToString();
    }
    
    public bool AddCard(CardPreset _card)
    {
        if (cardCount < 12)
        {
            deck.Add(_card);
            cardCount++;
            //Debug.Log("Card added, Total: " + cardCount);
            cardCountText.text = cardCount.ToString();
            return true;
        }
        else
        {
            //Debug.Log("Deck is full!");
            return false;
        }
    }

    public bool RemoveCard(CardPreset _card)
    {
        deck.Remove(_card);
        cardCount--;
        //Debug.Log("Card removed, Total: " + cardCount);
        cardCountText.text = cardCount.ToString();
        return false;
    }

    public void ToGame()
    {
        if(cardCount == 12)
        {
            switch (playerID)
            {
                case 1:
                    //transmit to player 1's deck in GameManager
                    playerID++;
                    playerIDText.text = playerID.ToString();
                    ClearDeck();
                    break;

                case 2:
                    //transmit to player 2's deck in GameManager
                    SceneManager.LoadScene(0);
                    break;

                default:
                    break;
            }
        }
    }

    public void ClearDeck()
    {
        //deck = new List<CardPreset>();
        foreach(CardSelector cs in cardSelectors)
        {
            if (cs.IsSelected)
                cs.ToggleAddRemove();

            if (cardCount == 0)
                break;
        }
    }
}
