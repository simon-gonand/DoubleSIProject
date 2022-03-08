using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Deck player1Deck;
    public Deck player2Deck;

    public List<CardPreset> deck1;
    public List<CardPreset> deck2;
    public CinemachineStateDrivenCamera stateDrivenCamera;

    public bool player1Turn = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);
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
        return LayerMask.NameToLayer("CardHandP2");
    }

    public void PlayCard(Card card)
    {
        if (player1Turn)
            player1Deck.PlayCard(card);
        else
            player2Deck.PlayCard(card);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainScene") return;
        Deck[] deck = FindObjectsOfType<Deck>();
        player1Deck = deck[0];
        player2Deck = deck[1];

        for (int i = 0; i < player1Deck.stack.Count; ++i)
        {
            player1Deck.stack[i].stats = deck1[i];
        }

        for (int i = 0; i < player2Deck.stack.Count; ++i)
        {
            player2Deck.stack[i].stats = deck2[i];
        }

        player1Deck.isPlayerOne = true;
        player2Deck.isPlayerOne = false;
        player1Deck.Draw();
        player2Deck.Draw();
    }
}