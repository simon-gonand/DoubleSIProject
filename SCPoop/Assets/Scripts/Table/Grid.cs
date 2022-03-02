using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    public List<Transform> slots;

    private Card[][] _cards = new Card[3][];
    public Card[][] cards { get { return _cards; } }

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
        for (int i = 0; i < 3; ++i)
            cards[i] = new Card[3];
    }

    public bool IsSlotEmpty(Transform t)
    {
        for(int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].position == t.position)
            {
                int y = i / 3;
                int x = i - y * 3;
                if (cards[x][y] == null) return true;
                else return false;
            }
        }
        return true;
    }

    public void AddCard(Card card)
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            if (Vector3.Distance(slots[i].position, card.self.position) < 0.01f)
            {
                int y = i / 3;
                int x = i - y * 3;
                cards[x][y] = card;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
