using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDetection : MonoBehaviour
{
    [SerializeField]
    private Camera selfCamera;

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

        Ray ray = selfCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Card")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                CardDragMovements card = hit.collider.gameObject.GetComponent<CardDragMovements>();
                if (card != null)
                {
                    if (card.isSnapped || card.card.isInStack || !GameManager.instance.CheckPlayerTurn(card.card)) return;
                    draggingCard = card;
                    draggingCard.InitDrag();
                    draggingCard.isDragged = true;
                }
            }
        }
    }
}
