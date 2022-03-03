using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private Transform stackPosition;
    [SerializeField]
    private Transform discardPosition;
    [SerializeField]
    private Transform[] handPositions;

    public List<Card> stack;
    private List<Card> discard = new List<Card>(12);
    private List<Card> hand = new List<Card>(6);
    private List<Card> playedCard = new List<Card>();

    private bool isDrawing = false;
    private bool isCardDrawing = false;
    private Card drawingCard;
    private Vector3 handPositionDrawing;
    private float movingT = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Draw()
    {
        if (hand.Count < 6)
        {
            if (stack.Count == 0)
                RefillStack();
            isDrawing = true;
            int index = Random.Range(0, stack.Count - 1);
            handPositionDrawing = GetCardPosition();
            hand.Add(stack[index]);
            isCardDrawing = true;
            drawingCard = stack[index];
            drawingCard.isInStack = false;
            movingT = 0.0f;
            stack.RemoveAt(index);
        }
        else
            isDrawing = false;
    }

    private void RefillStack()
    {
        stack.AddRange(discard);
        discard.Clear();
    }

    private Vector3 GetCardPosition()
    {
        foreach(Transform t in handPositions)
        {
            bool emptySlot = true;
            foreach(Card card in hand)
            {
                if (Vector3.Distance(t.position, card.self.position) < 0.01f)
                {
                    emptySlot = false;
                    break;
                }
            }
            if (emptySlot)
            {
                return t.position;
            }
        }
        return Vector3.positiveInfinity;
    }

    public void PlayCard(Card card)
    {
        hand.Remove(card);
        playedCard.Add(card);
    }

    public void Discard()
    {
        while (playedCard.Count > 0)
        {
            StartCoroutine(DiscardMovement(playedCard[0]));
            playedCard[0].GetComponent<CardDragMovements>().Initialize();
            discard.Add(playedCard[0]);
            playedCard.Remove(playedCard[0]);
        }
    }

    private IEnumerator DiscardMovement(Card card)
    {
        float t = 0.0f;
        Vector3 startPos = card.self.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * 10.0f;
            card.self.position = Vector3.Lerp(startPos, discardPosition.position, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            if (isCardDrawing)
            {
                movingT += Time.deltaTime * 10;
                drawingCard.self.position = Vector3.Lerp(stackPosition.position, handPositionDrawing, movingT);
                if (movingT > 1.0f)
                {
                    movingT = 0.0f;
                    isCardDrawing = false;
                }
            }
            else
            {
                Draw();
            }
        }
    }
}
