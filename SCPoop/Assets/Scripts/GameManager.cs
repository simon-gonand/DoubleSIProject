using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Deck playerDeck;

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
        playerDeck.Draw();   
    }

    public void EndTurn()
    {
        playerDeck.Discard();
        playerDeck.Draw();
        Grid.instance.BeginTurn();
    }

    public void PlayCard(Card card)
    {
        playerDeck.PlayCard(card);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
