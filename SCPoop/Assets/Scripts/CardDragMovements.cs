using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragMovements : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    private Vector3 offset;
    private float zCoord;
    private float xOriginalRotation;
    private Vector3 originalPos;

    private bool _isDragged = false;
    public bool isDragged { set { _isDragged = value; } }

    public void InitDrag()
    {
        originalPos = self.position;
        zCoord = Camera.main.WorldToScreenPoint(self.position).z;
        offset = self.position - GetMouseWorldPosition();

        xOriginalRotation = self.eulerAngles.x;
        self.rotation = Quaternion.Euler(0.0f, self.eulerAngles.y, self.eulerAngles.z);
    }

    public void EndDrag()
    {
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

        Debug.DrawRay(self.position, Camera.main.transform.forward * 100);
        Vector3 newPos = GetMouseWorldPosition() + offset;
        float yOffset = newPos.y - self.position.y;
        newPos.y = self.position.y;
        newPos.z += yOffset;
        self.position = newPos;

        CheckSnapToSlot();
    }

    private void CheckSnapToSlot()
    {

    }
}
