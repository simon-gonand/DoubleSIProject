using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Deck playerDeck;

    // Start is called before the first frame update
    void Start()
    {
        playerDeck.Draw();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
