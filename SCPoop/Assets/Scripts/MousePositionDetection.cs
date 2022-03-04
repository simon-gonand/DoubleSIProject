using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDetection : MonoBehaviour
{
    private CardDragMovements draggingCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && draggingCard != null)
        {
            draggingCard.isDragged = false;
            draggingCard.EndDrag();
            draggingCard = null;
        }

        if (draggingCard != null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << GameManager.instance.GetPlayerLayerMask()))
            {
                CardDragMovements card = hit.collider.gameObject.GetComponent<CardDragMovements>();
                if (card != null)
                {
                    if (card.isSnapped || card.card.isInStack) return;
                    draggingCard = card;
                    draggingCard.InitDrag();
                    draggingCard.isDragged = true;
                }
            }
        }
    }
}
