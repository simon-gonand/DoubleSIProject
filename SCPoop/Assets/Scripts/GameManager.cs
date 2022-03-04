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
        player1Deck.isPlayerOne = true;
        player2Deck.isPlayerOne = false;
        player1Deck.Draw();   
        player2Deck.Draw();   
    }

    public void EndTurn()
    {
        player1Deck.Discard();
        player2Deck.Discard();
    }

    public LayerMask GetPlayerLayerMask()
    {
        if (player1Turn)
            return LayerMask.NameToLayer("CardHandP1");
        return LayerMask.NameToLayer("CardHandP1");
    }

    public void PlayCard(Card card)
    {
        if (player1Turn)
            player1Deck.PlayCard(card);
        else
            player2Deck.PlayCard(card);
    }
}