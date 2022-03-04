using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Deck player1Deck;
    public Deck player2Deck;

    public bool player1Turn = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player1Deck.Draw();   
        player2Deck.Draw();   
    }

    public void EndTurn()
    {
        player1Deck.Discard();
        player2Deck.Discard();
    }

    public bool CheckPlayerTurn(Card card)
    {
        if ((player1Turn && LayerMask.LayerToName(card.gameObject.layer) == "CardInHand1") ||
            (!player1Turn && LayerMask.LayerToName(card.gameObject.layer) == "CardInHand2"))
            return true;
        return false;
    }

    public void PlayCard(Card card)
    {
        if (player1Turn)
            player1Deck.PlayCard(card);
        else
            player2Deck.PlayCard(card);
    }
}