using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDetection : MonoBehaviour
{
    private CardDragMovements draggingCard;

    private CardDragMovements hoveredCard;

    private bool canHover = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && draggingCard != null)
        {
            StartCoroutine(HoverCooldown());
            draggingCard.isDragged = false;
            draggingCard.EndDrag();
            draggingCard = null;
        }

        if (draggingCard != null) return;

        Ray ray = CameraSwitch.instance.currentCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << GameManager.instance.GetPlayerLayerMask()))
        {
            CardDragMovements card = hit.collider.gameObject.GetComponent<CardDragMovements>();

            if (!Input.GetMouseButton(0) && canHover && !GameManager.instance.player1Deck.isDrawing && !GameManager.instance.player2Deck.isDrawing && !GameManager.instance.stateDrivenCamera.IsBlending)
            {
                if (card != hoveredCard)
                    EndCurrentHover();

                hoveredCard = card;

                GameManager.instance.tooltipUI.DisplayInfo(hit.collider.gameObject.GetComponent<Card>());

                if (!hoveredCard.isHover)
                {
                    hoveredCard.OnBeginHover();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (card != null)
                {
                    EndCurrentHover();

                    if (card.isSnapped || card.card.isInStack) return;
                    draggingCard = card;
                    draggingCard.InitDrag();
                    draggingCard.isDragged = true;
                }
            }
        }
        else
            EndCurrentHover();

        if (GameManager.instance.stateDrivenCamera.IsBlending)
        {
            EndCurrentHover();
        }

    }

    private void EndCurrentHover()
    {
        if (hoveredCard != null)
        {
            GameManager.instance.tooltipUI.CleanInfo();
            hoveredCard.OnEndHover();
            hoveredCard = null;
        }
    }

    private IEnumerator HoverCooldown()
    {
        canHover = false;

        yield return new WaitForSeconds(0.1f);

        canHover = true;
    }
}
