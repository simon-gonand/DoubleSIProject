using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragMovements : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private Card card;

    private Vector3 offset;
    private float zCoord;
    private float xOriginalRotation;
    private Vector3 originalPos;

    private bool _isDragged = false;
    public bool isDragged { set { _isDragged = value; } }

    public bool isSnapped;

    public void InitDrag()
    {
        if (isSnapped) return;
        originalPos = self.position;
        zCoord = Camera.main.WorldToScreenPoint(self.position).z;
        offset = self.position - GetMouseWorldPosition();

        xOriginalRotation = self.eulerAngles.x;
        self.rotation = Quaternion.Euler(0.0f, self.eulerAngles.y, self.eulerAngles.z);
    }

    public void EndDrag()
    {
        if (isSnapped)
        {
            Grid.instance.AddCard(card);
            return;
        }
        self.rotation = Quaternion.Euler(xOriginalRotation, self.eulerAngles.y, self.eulerAngles.z);
        StartCoroutine(LerpToPosition(originalPos));
    }

    private IEnumerator LerpToPosition(Vector3 position)
    {
        float t = 0.0f;
        Vector3 startPos = self.position;
        while(t <= 1.0f)
        {
            t += Time.deltaTime * 10;
            self.position = Vector3.Lerp(startPos, position, t);
            yield return null;
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    // Update is called once per frame
    void Update()
    {
        if (!_isDragged) return;

        Debug.DrawRay(GetMouseWorldPosition(), Camera.main.transform.forward * 100);

        if (!isSnapped)
        {
            Vector3 newPos = GetMouseWorldPosition() + offset;
            float yOffset = newPos.y - self.position.y;
            newPos.y = self.position.y;
            newPos.z += yOffset;
            self.position = newPos;
        }

        CheckSnapToSlot();
    }

    private void CheckSnapToSlot()
    {
        RaycastHit hit;
        if (Physics.Raycast(GetMouseWorldPosition(), Camera.main.transform.forward, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Slot")))
        {
            if (isSnapped) return;
            if (!Grid.instance.IsSlotEmpty(hit.collider.transform)) return;
            Vector3 snapPos = hit.collider.transform.position;
            snapPos.y += 0.01f;
            self.position = snapPos;
            isSnapped = true;
        }
        else
        {
            isSnapped = false;
            Vector3 upPos = self.position;
            upPos.y = originalPos.y;
            self.position = upPos;
        }
    }
}
