using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragMovements : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    public Card card;

    private float xOriginalRotation;
    private Vector3 originalPos;

    private bool _isDragged = false;
    public bool isDragged { set { _isDragged = value; } }

    public bool isSnapped;

    public void Initialize()
    {
        _isDragged = false;
        isSnapped = false;
    }

    public void InitDrag()
    {
        if (isSnapped) return;

        originalPos = self.position;
        xOriginalRotation = self.eulerAngles.x;
        self.rotation = Quaternion.Euler(0.0f, self.eulerAngles.y, self.eulerAngles.z);
    }

    public void EndDrag()
    {
        if (isSnapped)
        {
            GameManager.instance.PlayCard(card);
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

    // Update is called once per frame
    void Update()
    {
        if (!_isDragged) return;

        Ray ray = CameraSwitch.instance.currentCamera.ScreenPointToRay(Input.mousePosition);
        if (!isSnapped)
        {
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("CardMovement"));
            Vector3 newPos = hit.point;
            self.position = newPos;
        }

        CheckSnapToSlot(ray);
    }

    private void CheckSnapToSlot(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Slot")))
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
