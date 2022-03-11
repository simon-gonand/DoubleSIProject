using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{
    public static Carousel instance;

    [SerializeField]
    private GameObject firstBoard;
    [SerializeField]
    private GameObject secondBoard;
    [SerializeField]
    private GameObject cardPrefab;

    private Vector3 playBoardPos;
    private Vector3 startBoardPos;
    private bool firstBoardPlay = true;

    public bool inAction = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playBoardPos = firstBoard.transform.localPosition;
            startBoardPos = secondBoard.transform.localPosition;
        }
        else
            Destroy(gameObject);
    }

    public void ChangeBoard(CardPreset[] cards)
    {
        inAction = true;

        if (firstBoardPlay)
        {
            StartCoroutine(ChangeBoardLerp(secondBoard, firstBoard, cards));
        }
        else
        {
            StartCoroutine(ChangeBoardLerp(firstBoard, secondBoard, cards));
        }
        firstBoardPlay = !firstBoardPlay;
    }

    private IEnumerator ChangeBoardLerp(GameObject boardToPlay, GameObject boardToRemove, CardPreset[] cards)
    {
        Transform[] slots = boardToPlay.GetComponentsInChildren<Transform>();

        List<Card> newCards = new List<Card>();

        for (int i = 0; i < 3; ++i)
        {
            GameObject c = Instantiate(cardPrefab, slots[i + 1].position, Quaternion.identity, slots[i + 1]);
            Card card = c.GetComponent<Card>();
            c.GetComponent<CardDragMovements>().isSnapped = true;
            card.stats = cards[i];
            card.GetComponent<Card>().Init();
            newCards.Add(card);
        }

        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * 2.0f;
            boardToPlay.transform.localPosition = Vector3.Lerp(startBoardPos, playBoardPos, t);
            boardToRemove.transform.localPosition = Vector3.Lerp(playBoardPos, new Vector3(playBoardPos.x + (playBoardPos.x - startBoardPos.x), playBoardPos.y, playBoardPos.z), t);
            yield return null;
        }

        boardToRemove.transform.localPosition = startBoardPos;

        Grid.instance.ChangeEnemyHand(newCards, slots);
        inAction = false;
    }
}
