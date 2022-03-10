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

    public TooltipUI tooltipUI;

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

    public void TimerEnd()
    {
        List<List<int>> freeSlots = new List<List<int>>();
        for (int i = 0; i < 3; ++i)
        {
            freeSlots.Add(new List<int>());
            for (int j = 1; j < 3; ++j)
            {
                if (Grid.instance.cards[i][j] == null)
                {
                    freeSlots[freeSlots.Count - 1].Add(j);
                }
            }
        }
        bool isLineEmpty = true;
        int x = 0;
        while (isLineEmpty)
        {
            x = Random.Range(0, 3);
            if (freeSlots[x].Count != 0)
                isLineEmpty = false;
        }
        if (player1Turn)
        {   
            StartCoroutine(player1Deck.PlayRandomCard(x, freeSlots[x][Random.Range(0, freeSlots[x].Count)]));
        }
        else
            StartCoroutine(player2Deck.PlayRandomCard(x, freeSlots[x][Random.Range(0, freeSlots[x].Count)]));
    }

    public void EndTurn()
    {
        player1Deck.Discard();
        player2Deck.Discard();
        Grid.instance.ClearPlayerTrails();
        LifeSystem.instance.CheckDeath();
    }

    public LayerMask GetPlayerLayerMask()
    {
        if (player1Turn)
            return LayerMask.NameToLayer("CardHandP1");
        return LayerMask.NameToLayer("CardHandP2");
    }

    public LayerMask GetBoardLayerMask()
    {
        return LayerMask.NameToLayer("Card");
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
        if (scene.name == "MainMenu" || scene.name == "DeckBuilding" || scene.name == "Death Screen")
        {
            Timer.instance.StopTimer();
            return;
        }
        Timer.instance.LaunchTimer();
        stateDrivenCamera = FindObjectOfType<CinemachineStateDrivenCamera>();
        tooltipUI = FindObjectOfType<TooltipUI>();
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